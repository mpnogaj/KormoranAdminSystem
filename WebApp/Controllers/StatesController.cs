using KormoranAdminSystemRevamped.Contexts;
using KormoranAdminSystemRevamped.Properties;
using KormoranShared.Models;
using KormoranShared.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KormoranAdminSystemRevamped.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly KormoranContext _db;

        public StatesController(KormoranContext db)
        {
            _db = db;
        }

        public async Task<JsonResult> GetStates()
        {
            try
            {
                var states = await _db.States.ToListAsync();
                return new JsonResult(new CollectionResponse<State>
                {
                    Collection = states,
                    Error = false,
                    Message = Resources.operationSuccessfull
                });
            }
            catch
            {
                return new JsonResult(new CollectionResponse<State>
                {
                    Collection = null,
                    Error = true,
                    Message = Resources.serverError
                });
            }
        }
    }
}
