using System.Collections.Generic;
using KormoranAdminSystemRevamped.Models;
using Microsoft.AspNetCore.Mvc;

namespace KormoranAdminSystemRevamped.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class LogController : ControllerBase
	{
		[HttpGet]
		public JsonResult GetLogs([FromQuery(Name = "sessionId")]string sessionId)
		{
			GetLogsResponse response = new GetLogsResponse
			{
				Error = false,
				LogEntries = new List<LogEntry>()
			};

			return new JsonResult(response);
		}
	}

	public record GetLogsResponse
	{
		public bool Error { get; set; }
		public List<LogEntry>? LogEntries { get; set; }
	}
}