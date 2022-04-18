using KormoranWeb.Contexts;
using KormoranWeb.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using KormoranShared.Models;
using KormoranShared.Models.Responses;
using KormoranWeb.Models;
using KormoranWeb.Models.Responses;

namespace KormoranWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private KormoranContext _db;

        public TeamsController(KormoranContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<JsonResult> GetTeams([FromQuery] int? tournamentId)
        {
            try
            {
                var teams = await _db.Teams.ToListAsync();
                if (tournamentId.HasValue)
                {
                    return new JsonResult(new CollectionResponse<Team>
                    {
                        Error = false,
                        Message = Resources.operationSuccessfull,
                        Collection = teams.Where(x => x.TournamentId == tournamentId).ToList()
                    });
                }
                else
                {
                    return new JsonResult(new CollectionResponse<Team>
                    {
                        Error = false,
                        Message = Resources.operationSuccessfull,
                        Collection = teams
                    });
                }
            }
            catch
            {
                return new JsonResult(new CollectionResponse<Team>
                {
                    Error = true,
                    Message = Resources.serverError,
                    Collection = null
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetTeam([FromQuery] int teamId)
        {
            try
            {
                var team = await _db.Teams.FirstOrDefaultAsync((x) => x.Id == teamId);
                return new JsonResult(new SingleItemResponse<Team>
                {
                    Error = false,
                    Message = Resources.operationSuccessfull,
                    Data = team
                });
            }
            catch
            {
                return new JsonResult(new SingleItemResponse<Team>
                {
                    Error = true,
                    Message = Resources.serverError,
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTeams([FromBody] Team team)
        {
            try
            {
                if (team.Id == 0)
                {
                    await _db.Teams.AddAsync(team);
                }
                else
                {
                    var t = await _db.Teams.FirstOrDefaultAsync(x => x.Id == team.Id);
                    if (t != null) t.Name = team.Name;
                }
                await _db.SaveChangesAsync();
                return new JsonResult(new SingleItemResponse<int>
                {
                    Error = false,
                    Message = Resources.operationSuccessfull,
                    Data = team.Id
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

        [HttpDelete]
        public async Task<JsonResult> DeleteTeam(int teamId)
        {
            try
            {
                var res = await _db.Teams.FirstOrDefaultAsync(x => x.Id == teamId);
                if (res != null)
                {
                    _db.Teams.Remove(res);
                    await _db.SaveChangesAsync();
                }
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
    }
}
