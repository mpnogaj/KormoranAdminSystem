using KormoranShared.Models;
using KormoranShared.Models.Requests.Matches;
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
	public class MatchesController : ControllerBase
	{
		private readonly KormoranContext _db;
		private readonly IConfiguration _configuration;
		private readonly ILogger _logger;

		public MatchesController(KormoranContext db, IConfiguration configuration, ILogger logger)
		{
			_db = db;
			_configuration = configuration;
			_logger = logger;
		}

		[HttpGet]
		public async Task<JsonResult> GetMatches([FromQuery] int? tournamentId)
		{
			try
			{
				var matches = await _db.Matches
					.Include(x => x.State)
					.Include(x => x.Team1)
					.Include(x => x.Team2)
					.OrderBy(x => x.MatchId)
					.ToListAsync();
				if (tournamentId.HasValue)
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
		[Authorize]
		public async Task<JsonResult> UpdateMatch([FromBody] Match match)
		{
			try
			{
				_db.Matches.Update(match);
				await _db.SaveChangesAsync();

				await _logger.LogNormal(new LogEntry(User.GetFullName(), $"Zaktualizował mecz: {match.MatchId}"));
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
		[Authorize]
		public async Task<JsonResult> UpdateMatches([FromBody] List<Match> matches)
		{
			try
			{
				_db.Matches.UpdateRange(matches);
				await _db.SaveChangesAsync();

				await _logger.LogNormal(new LogEntry(User.GetFullName(), $"Zaktualizował {matches.Count} meczy"));
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
		[Authorize]
		public async Task<JsonResult> UpdateScore([FromBody] UpdateScoreRequestModel request)
		{
			try
			{
				var match = await _db.Matches.FirstOrDefaultAsync(x => x.MatchId == request.MatchId);
				if (match == null)
				{
					return new JsonResult(new BasicResponse
					{
						Error = true,
						Message = $"Nie znaleziono meczu"
					});
				}
				match.StateId = Convert.ToInt32(_configuration["StateIDs:Finished"]);
				match.Team1Score = request.Team1Score;
				match.Team2Score = request.Team2Score;
				_db.Matches.Update(match);
				await _db.SaveChangesAsync();
				await _logger.LogMajor(new LogEntry(User.GetFullName(), $"Zaktualizował wynik meczu: {match.MatchId} | {match.Team1Score}:{match.Team2Score}"));
				return new JsonResult(new BasicResponse
				{
					Error = false,
					Message = Resources.operationSuccessfull
				});
			}
			catch (Exception ex)
			{
				return new JsonResult(new BasicResponse
				{
					Error = true,
					Message = $"{Resources.serverError}. Exception: {ex.Message}"
				});
			}
		}
	}
}