using KormoranShared.Models.Responses;
using KormoranWeb.Contexts;
using KormoranWeb.Helpers;
using KormoranWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KormoranWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly KormoranContext _db;
        private readonly ISessionManager _sessionManager;

        public SessionController(KormoranContext db, ISessionManager sessionManager)
        {
            _db = db;
            _sessionManager = sessionManager;
        }

        [HttpPost]
        public async Task<JsonResult> Login(LoginRequestModel model)
        {
            var response = new LoginResponseModel();
            try
            {
                if (string.IsNullOrEmpty(model.Username) && string.IsNullOrEmpty(model.Password))
                {
                    response.Error = true;
                    response.Message = "Niewystarczające dane";
                }
                else
                {
                    var user = await _db.Users.FirstOrDefaultAsync(x => x.Login == model.Username);
                    if (user != null)
                    {
                        string thisPasswordHash = model.Password.Sha256().ToUpper();
                        if (user.PasswordHash.ToUpper() != thisPasswordHash)
                        {
                            response.Error = true;
                            response.Message = "Niepoprawne hasło";
                        }
                        else
                        {
                            var sessionGuid = _sessionManager.CreateSession(model.Username);
                            response.Error = false;
                            response.Message = "Zalogowano pomyślnie";
                            response.SessionId = sessionGuid;
                        }
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Niepoprawna nazwa użytkownika";
                    }
                }
            }
            catch (Exception e)
            {
                response.Error = true;
                response.Message = e.Message;
            }

            return new JsonResult(response);
        }

        [HttpPost]
        public IActionResult Logout(LogoutRequestModel model)
        {
            _sessionManager.ExpireSession(model.SessionId);
            return StatusCode(200);
        }

        [HttpGet]
        public JsonResult Validate(string sessionId)
        {
            var response = new BasicResponse();
            if (_sessionManager.GetSession(sessionId) != null)
            {
                response.Error = false;
                response.Message = "Sesja jest ważna";
            }
            else
            {
                response.Error = true;
                response.Message = "Sesja wygasła. Zaloguj się ponownie";
            }

            return new JsonResult(response);
        }
    }

    public record LoginRequestModel
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public record LogoutRequestModel
    {
        public string SessionId { get; set; } = "";
    }

    public class LoginResponseModel : BasicResponse
    {
        public string SessionId { get; set; } = "";
    }
}