using KormoranAdminSystemRevamped.Contexts;
using KormoranAdminSystemRevamped.Models;
using KormoranAdminSystemRevamped.Models.Responses;
using KormoranAdminSystemRevamped.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KormoranAdminSystemRevamped.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class MatchesController : ControllerBase
	{
		private readonly KormoranContext _db;

		public MatchesController(KormoranContext db)
		{
			_db = db;
		}

		[HttpGet]
		public async Task<JsonResult> GetMatches([FromQuery] int? tournamentId)
		{
			try
			{
				var matches = await _db.Matches.ToListAsync();
				if(tournamentId.HasValue)
                {
					return new JsonResult(new CollectionResponse<Match>
					{
						Error = false,
						Message = Resources.operationSuccessfull,
						Collection = matches
							.Where(x => x.TournamentId == tournamentId.Value)
							.ToList()
					});
				}
				return new JsonResult(new CollectionResponse<Match>
				{
					Error = false,
					Message = Resources.operationSuccessfull,
					Collection = matches
				});
            }
			catch
			{
				return new JsonResult(new CollectionResponse<Match>
				{
					Error = true,
					Message = Resources.serverError,
					Collection = null
				});
			}
		}

		[HttpGet]
		public async Task<JsonResult> GetMatch([FromQuery] int matchId)
		{
			try
			{
				var match = await _db.Matches.FirstOrDefaultAsync(x => x.MatchId == matchId);
				return new JsonResult(new SingleItemResponse<Match>
				{
					Error = false,
					Message = Resources.operationSuccessfull,
					Data = match
				});
			}
			catch
			{
				return new JsonResult(new SingleItemResponse<Match>
				{
					Error = true,
					Message = Resources.serverError,
					Data = null
				});
			}
		}

		[HttpPost]
		public async Task<JsonResult> UpdateMatch([FromBody] Match match)
        {
            try
            {
				_db.Matches.Update(match);
				await _db.SaveChangesAsync();
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
					Message = Resources.serverError,
				});
            }
        }

		[HttpPost]
		public async Task<JsonResult> UpdateMatches([FromBody]List<Match> matches)
        {
			try
			{
				_db.Matches.UpdateRange(matches);
				await _db.SaveChangesAsync();
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
					Message = Resources.serverError,
				});
			}
		}
    }
}
