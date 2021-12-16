using KormoranAdminSystemRevamped.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using KormoranAdminSystemRevamped.Helpers;
using KormoranAdminSystemRevamped.Services;

namespace KormoranAdminSystemRevamped.Controllers
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
			return new JsonResult(response);
		}

		[HttpPost]
		public IActionResult Logout(string sessionId)
		{
			_sessionManager.ExpireSession(sessionId);
			return StatusCode(200);
		}

		[HttpGet]
		public JsonResult Validate(string sessionId)
		{
			var response = new ValidateResponseModel();
			if (_sessionManager.GetSession(sessionId) != null)
			{
				response.IsValid = true;
				response.Message = "Sesja jest ważna";
			}
			else
			{
				response.IsValid = false;
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

	public record LoginResponseModel
	{
		public bool Error { get; set; }
		public string Message { get; set; } = "";
		public string SessionId { get; set; } = "";
	}

	public record ValidateResponseModel
	{
		public bool IsValid { get; set; }
		public string Message { get; set; } = "";
	}
}
