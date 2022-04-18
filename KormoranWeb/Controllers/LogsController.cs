using KormoranWeb.Contexts;
using KormoranWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using KormoranShared.Models;
using KormoranShared.Models.Responses;
using KormoranWeb.Models;
using KormoranWeb.Models.Responses;

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
                    Message = "Musisz być zalogowany, żeby zobaczyć logi!",
                    Collection = null
                });
            }
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