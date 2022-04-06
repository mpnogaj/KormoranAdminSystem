using KormoranShared.Models;
using KormoranShared.Models.Responses;
using KormoranWeb.Contexts;
using KormoranWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace KormoranWeb.Controllers
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
        public JsonResult GetLogs([FromQuery(Name = "sessionId")] string sessionId)
        {
            if (_sessionManager.GetSession(sessionId) == null)
            {
                return new JsonResult(new CollectionResponse<LogEntry>
                {
                    Error = true,
                    Message = "Musisz byæ zalogowany, ¿eby zobaczyæ logi!",
                    Collection = null
                });
            }
            try
            {
                return new JsonResult(new CollectionResponse<LogEntry>
                {
                    Error = false,
                    Message = "Operacja zakoñczona sukcesem",
                    Collection = _dbContext.Logs.ToList()
                });
            }
            catch
            {
                return new JsonResult(new CollectionResponse<LogEntry>
                {
                    Error = true,
                    Message = "B³¹d po stronie serwera. Powiadom administratora",
                    Collection = null
                });
            }

        }
    }
}