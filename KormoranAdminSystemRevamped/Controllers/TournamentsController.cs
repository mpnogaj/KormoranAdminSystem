using System.Collections.Generic;
using System.Linq;
using KormoranAdminSystemRevamped.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KormoranAdminSystemRevamped.Models;

using System.Threading.Tasks;
using KormoranAdminSystemRevamped.Services;

namespace KormoranAdminSystemRevamped.Controllers
{
	[Route(@"api/[controller]/[action]")]
	[ApiController]
	public class TournamentsController : ControllerBase
	{
		private readonly KormoranContext _db;
		private readonly ISessionManager _sessionManager;
		public TournamentsController(KormoranContext dp, ISessionManager sessionManager)
		{
			_db = dp;
			_sessionManager = sessionManager;
		}

		[HttpGet]
		public async Task<JsonResult> GetTournaments([FromQuery]TournamentRequestModel model)
		{
			var tournamentList = await _db.Tournaments
				.Include(x => x.State)
				.Include(x => x.Discipline)
				.Include(x => x.Teams)
				.Include(x => x.Matches)
				.ToListAsync();
			if (model.StateId != null)
			{
				tournamentList = tournamentList.Where(x => x.State.Id == model.StateId).ToList();
			}

			return new JsonResult(new TournamentResponseModel()
			{
				Tournaments = tournamentList
			});
		}

		[HttpGet]
		public async Task<JsonResult> GetMatches([FromQuery] int id)
		{
			var tournament = await _db.Tournaments
				.Include(x => x.Discipline)
				.Include(x => x.State)
				.Include(x => x.Teams)
				.Include(x => x.Matches)
				.FirstAsync(x => x.Id == id);
			
			if (tournament == null)
			{
				return new JsonResult(new GetMatchesResponseModel
				{
					Error = true,
					Matches = null
				});
			}

			return new JsonResult(new GetMatchesResponseModel
			{
				Error = false,
				Matches = tournament.Matches.ToList()
			});
		}

		[HttpPost]
		public async Task<JsonResult> AddEdit(AddEditRequestModel model)
		{
			var response = new AddEditResponseModel();
			if (_sessionManager.GetSession(model.SessionId) == null)
			{
				response.Error = true;
				response.Message = "Sesja wygasła. Zaloguj się ponownie";
				return new JsonResult(response);
			}

			response.Error = false;
			response.Message = "Operacja zakończona sukcesem";
			var tournament = new Tournament
			{
				Name = model.Name,
				Discipline = _db.Disciplines.FirstOrDefault(x => x.Id == model.DisciplineId),
				State = _db.States.FirstOrDefault(x => x.Id == model.StateId),
				TournamentType = model.TournamentType,
				TournamentTypeShort = model.TournamentTypeShort
			};
			if (model.Id == -1)
			{
				await _db.Tournaments.AddAsync(tournament);
				await _db.SaveChangesAsync();
			}
			else
			{
				var toReplace = 
					await _db.Tournaments.FirstOrDefaultAsync(x => x.Id == model.Id);
				if (toReplace != null)
				{
					tournament.Id = model.Id;
					toReplace.Discipline = tournament.Discipline;
					toReplace.Name = tournament.Name;
					toReplace.State = tournament.State;
					toReplace.TournamentType = tournament.TournamentType;
					toReplace.TournamentTypeShort = tournament.TournamentTypeShort;
					await _db.SaveChangesAsync();
				}
				else
				{
					response.Error = true;
					response.Message = "Turniej nie istnieje";
				}
			}

			return new JsonResult(response);
		}
	}
	
	public record TournamentRequestModel
	{
		[FromQuery(Name = "State")]
		public int? StateId { get; set; }
	}

	public record TournamentResponseModel
	{
		public List<Tournament> Tournaments { get; set; }
	}

	public record AddEditRequestModel
	{
		public string SessionId { get; set; }
		public int Id { get; set; }
		public int StateId { get; set; }
		public int DisciplineId { get; set; }
		public string Name { get; set; }
		public string TournamentType { get; set; }
		public string TournamentTypeShort { get; set; }
	}

	public record AddEditResponseModel
	{
		public bool Error { get; set; }
		public string Message { get; set; }
	}

	public record GetMatchesResponseModel
	{
		public bool Error { get; set; }
		public List<Match>? Matches { get; set; }
	}
}
