using KormoranShared.Models;
using KormoranShared.Models.Responses;
using KormoranWeb.Contexts;
using KormoranWeb.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
		[Authorize]
		public JsonResult GetLogs()
		{
			try
			{
				return new JsonResult(new CollectionResponse<LogEntry>
				{
					Error = false,
					Message = Resources.operationSuccessfull,
					Collection = _dbContext.Logs.ToList()
				});
			}
			catch
			{
				return new JsonResult(new CollectionResponse<LogEntry>
				{
					Error = true,
					Message = Resources.serverError,
					Collection = null
				});
			}
		}
	}
}