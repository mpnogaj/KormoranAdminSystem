using System.Collections.Generic;
using System.Linq;
using KormoranAdminSystemRevamped.Contexts;
using KormoranAdminSystemRevamped.Models;
using KormoranAdminSystemRevamped.Services;
using Microsoft.AspNetCore.Mvc;

namespace KormoranAdminSystemRevamped.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class LogsController : ControllerBase
	{
		private readonly KormoranContext _dbContext;
		private readonly ISessionManager _sessionManager;
		public LogsController(KormoranContext dbContext, ISessionManager sessionManager)
		{
			_dbContext = dbContext;
			_sessionManager = sessionManager;
		}

		[HttpGet]
		public JsonResult GetLogs([FromQuery(Name = "sessionId")]string sessionId)
		{
			if (_sessionManager.GetSession(sessionId) == null)
			{
				return new JsonResult(new GetLogsResponse
				{
					Error = true,
					Message = "Musisz byæ zalogowany, ¿eby zobaczyæ logi!",
					LogEntries = null
				});
			}
			try
			{
				return new JsonResult(new GetLogsResponse
				{
					Error = false,
					Message = "Operacja zakoñczona sukcesem",
					LogEntries = _dbContext.Logs.ToList()
				});
			}
			catch
			{
				return new JsonResult(new GetLogsResponse 
				{ 
					Error = true, 
					Message = "B³¹d po stronie serwera. Powiadom administratora",
					LogEntries = null
				});
			}
			
		}
	}

	public record GetLogsResponse
	{
		public bool Error { get; set; }
		public string Message { get; set;} = "";
		public List<LogEntry>? LogEntries { get; set; }
	}
}