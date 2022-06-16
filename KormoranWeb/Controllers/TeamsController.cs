using KormoranShared.Models;
using KormoranShared.Models.Responses;
using KormoranWeb.Contexts;
using KormoranWeb.Helpers;
using KormoranWeb.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ILogger = KormoranWeb.Services.ILogger;

namespace KormoranWeb.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class TeamsController : ControllerBase
	{
		private readonly KormoranContext _db;
		private readonly ILogger _logger;

		public TeamsController(KormoranContext db, ILogger logger)
		{
			_db = db;
			_logger = logger;
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
		[Authorize]
		public async Task<JsonResult> UpdateTeams([FromBody] Team team)
		{
			try
			{
				if (team.Id == 0)
				{
					await _db.Teams.AddAsync(team);
					await _logger.LogMajor(new LogEntry(User.GetFullName(), $"Dodał drużynę: {team.Name}"));
				}
				else
				{
					var t = await _db.Teams.FirstOrDefaultAsync(x => x.Id == team.Id);
					if (t != null)
					{
						t.Name = team.Name;
						_db.Teams.Update(t);
						await _logger.LogNormal(new LogEntry(User.GetFullName(), $"Edytował drużynę: {team.Name}, {team.Id}"));
					}
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
		[Authorize]
		public async Task<JsonResult> DeleteTeam(int teamId)
		{
			try
			{
				var res = await _db.Teams.FirstOrDefaultAsync(x => x.Id == teamId);
				if (res != null)
				{
					_db.Teams.Remove(res);
					await _db.SaveChangesAsync();
					await _logger.LogMajor(new LogEntry(User.GetFullName(), $"Usunął drużynę: {res.Name}"));
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