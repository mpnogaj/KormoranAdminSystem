using KormoranShared.Models;
using KormoranShared.Models.Responses;
using KormoranWeb.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace KormoranWeb.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class LogsController : ControllerBase
	{
		private readonly KormoranContext _dbContext;

		public LogsController(KormoranContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public JsonResult GetLogs([FromQuery(Name = "sessionId")] string sessionId)
		{
			try
			{
				return new JsonResult(new CollectionResponse<LogEntry>
				{
					Error = false,
					Message = "Operacja zakończona sukcesem",
					Collection = _dbContext.Logs.ToList()
				});
			}
			catch
			{
				return new JsonResult(new CollectionResponse<LogEntry>
				{
					Error = true,
					Message = "Błąd po stronie serwera. Powiadom administratora",
					Collection = null
				});
			}
		}
	}
}