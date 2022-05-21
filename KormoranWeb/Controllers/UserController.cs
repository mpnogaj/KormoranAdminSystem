using KormoranShared.Models;
using KormoranShared.Models.Requests;
using KormoranShared.Models.Responses;
using KormoranWeb.Contexts;
using KormoranWeb.Helpers;
using KormoranWeb.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KormoranWeb.Controllers;

[Route(@"api/[controller]/[action]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly KormoranContext _kormoranContext;
    private readonly IConfiguration _configuration;

    public UserController(KormoranContext kormoranContext, IConfiguration configuration)
    {
        _kormoranContext = kormoranContext;
        _configuration = configuration;
    }

    public IActionResult Ping()
    {
        return Ok("Ping");
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<JsonResult> Login(AuthenticateRequest user, bool useCookie = false)
    {
        var u = await Authenticate(user);
        if (u != null)
        {
            var token = Generate(u);
            if (useCookie)
            {
                Response.Cookies.Append("Authorization", token, new CookieOptions
                {
                    HttpOnly = true
                });
                return new JsonResult(new BasicResponse
                {
                    Error = false,
                    Message = Resources.operationSuccessfull
                });
            }
            else
            {
                return new JsonResult(new AuthenticateResponse
                {
                    Error = false,
                    Message = Resources.operationSuccessfull,
                    Token = Generate(u)
                });
            }
        }
        return new JsonResult(new AuthenticateResponse
        {
            Error = true,
            Message = "Użytkownik nie został znaleziony",
            Token = string.Empty
        });
    }

    public async Task<JsonResult> IsAdmin()
    {
        var username = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return new JsonResult(new AdminCheckResponse
        {
            IsAdmin = (await CheckIsAdmin(username))
        });
    }

    private string Generate(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, user.Login),
            new(ClaimTypes.Name, user.Fullname)
        };

        var token = new JwtSecurityToken(
            _configuration["JWT:Issuer"],
            _configuration["JWT:Audience"],
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<bool> CheckIsAdmin(string username)
    {
        var user = await _kormoranContext.Users.FirstOrDefaultAsync(x => x.Login == username);
        return user != null && user.IsAdmin;
    }

    private async Task<User?> Authenticate(AuthenticateRequest authRequest)
    {
        var currUser = await _kormoranContext.Users
            .FirstOrDefaultAsync(x =>
                x.Login == authRequest.Username &&
                x.PasswordHash == authRequest.Password
                    .Sha256()
                    .ToLower()
                );
        return currUser;
    }
}