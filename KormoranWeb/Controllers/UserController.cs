using KormoranShared.Models;
using KormoranShared.Models.Requests;
using KormoranShared.Models.Responses;
using KormoranWeb.Contexts;
using KormoranWeb.Helpers;
using KormoranWeb.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ILogger = KormoranWeb.Services.ILogger;

namespace KormoranWeb.Controllers;

[Route(@"api/[controller]/[action]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
	private readonly KormoranContext _kormoranContext;
	private readonly IConfiguration _configuration;
	private readonly ILogger _logger;

	private const string AuthCookie = "Authorization";

	public UserController(KormoranContext kormoranContext, IConfiguration configuration, ILogger logger)
	{
		_kormoranContext = kormoranContext;
		_configuration = configuration;
		_logger = logger;
	}

	[HttpGet]
	//Important
	//Used in front to check if user is logged in (xd?)
	public IActionResult Ping()
	{
		return Ok("Ping");
	}

	[AllowAnonymous]
	[HttpPost]
	public async Task<JsonResult> Login(AuthenticateRequest user, bool useCookie = false)
	{
		var u = await Authenticate(user);
		if (u == null)
		{
			return new JsonResult(new AuthenticateResponse
			{
				Error = true,
				Message = "Bledny login lub haslo",
				Token = string.Empty
			});
		}
		var token = Generate(u);

		await _logger.LogNormal(new LogEntry(u.Fullname, "Zalogowal sie"));

		if (!useCookie)
		{
			return new JsonResult(new AuthenticateResponse
			{
				Error = false,
				Message = Resources.operationSuccessfull,
				Token = Generate(u)
			});
		}

		Response.Cookies.Append(AuthCookie, token, new CookieOptions
		{
			HttpOnly = true
		});
		return new JsonResult(new BasicResponse
		{
			Error = false,
			Message = Resources.operationSuccessfull
		});

	}

	[HttpGet]
	public async Task<JsonResult> GetUserInfo()
	{
		var username = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
		var user = await _kormoranContext.Users.FirstOrDefaultAsync(x => x.Login == username);
		if (user == null)
		{
			return new JsonResult(new SingleItemResponse<User?>
			{
				Message = "Couldn't find user with this username",
				Error = true,
				Data = null
			});
		}
		return new JsonResult(new SingleItemResponse<User>
		{
			Message = Resources.operationSuccessfull,
			Error = false,
			Data = user
		});
	}

	[HttpGet]
	public async Task<JsonResult> GetUsers()
	{
		var users = await _kormoranContext.Users.ToListAsync();
		return new JsonResult(new CollectionResponse<User>
		{
			Error = false,
			Message = Resources.operationSuccessfull,
			Collection = users
		});
	}

	[HttpGet]
	public async Task<JsonResult> IsAdmin()
	{
		var username = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
		return new JsonResult(new AdminCheckResponse
		{
			IsAdmin = (await CheckIsAdmin(username))
		});
	}

	[HttpDelete] //such innovation, much wow
	public async Task<JsonResult> DeleteUser([FromQuery] int userId)
	{
		var user = await _kormoranContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
		if (user == null)
		{
			return new JsonResult(new BasicResponse
			{
				Error = true,
				Message = "Couldn't find user with this id"
			});
		}
		_kormoranContext.Users.Remove(user);
		await _kormoranContext.SaveChangesAsync();
		await _logger.LogMajor(new LogEntry(User.GetFullName(), $"Usunął użytkownika: {user.Login}"));
		return new JsonResult(new BasicResponse
		{
			Error = false,
			Message = Resources.operationSuccessfull
		});
	}

	[HttpPost]
	public async Task<JsonResult> AddEditUser([FromBody] AddUserRequestModel model)
	{
		var newUser = new User
		{
			Id = model.Id,
			Fullname = model.Fullname,
			Login = model.Login,
			PasswordHash = model.Password.Sha256(),
			IsAdmin = model.IsAdmin
		};
		if (model.Id == 0)
		{
			_kormoranContext.Users.Add(newUser);
			await _logger.LogMajor(new LogEntry(User.GetFullName(), $"Dodał użytkownika: {newUser.Login}"));
		}
		else
		{
			var oldUser = await _kormoranContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
			if (oldUser == null)
			{
				return new JsonResult(new SingleItemResponse<int>
				{
					Error = true,
					Message = "Couldn't find user with this id",
					Data = -1
				});
			}
			//copy over new password if needed
			newUser.PasswordHash = model.Password == string.Empty ? oldUser.PasswordHash : newUser.PasswordHash;
			_kormoranContext.Users.Update(newUser);
			await _logger.LogNormal(new LogEntry(User.GetFullName(), $"Edytował użytkownika: {newUser.Login}"));
		}
		await _kormoranContext.SaveChangesAsync();
		return new JsonResult(new SingleItemResponse<int>
		{
			Error = false,
			Message = Resources.operationSuccessfull,
			Data = newUser.Id
		});
	}

	[HttpGet]
	public JsonResult GetFullName()
	{
		return new JsonResult(new SingleItemResponse<string>
		{
			Error = false,
			Message = Resources.operationSuccessfull,
			Data = User.GetFullName()
		});
	}

	[HttpPost]
	public async Task<JsonResult> Logout()
	{
		try
		{
			await _logger.LogMinor(new LogEntry(User.GetFullName(), "Wylogował się"));
			Response.Cookies.Delete(AuthCookie);
			return new JsonResult(new BasicResponse
			{
				Error = false,
				Message = Resources.operationSuccessfull
			});
		}
		catch
		{
			return new JsonResult(new BasicResponse
			{
				Error = true,
				Message = Resources.serverError
			});
		}
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