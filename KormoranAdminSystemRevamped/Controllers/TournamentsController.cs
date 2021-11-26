using System.Linq;
using KormoranAdminSystemRevamped.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KormoranAdminSystemRevamped.Models;

using System.Threading.Tasks;
using KormoranAdminSystemRevamped.Services;

namespace KormoranAdminSystemRevamped.Controllers
{
	[Route("api/[controller]")]
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
		public async Task<JsonResult> Tournaments([FromQuery]TournamentRequestModel model)
		{
			var tournamentList = await _db.Tournaments.ToListAsync();
			if (!string.IsNullOrWhiteSpace(model.State))
			{
				tournamentList = tournamentList.Where(x => x.State == model.State).ToList();
			}

			return new JsonResult(new
			{
				tournaments = tournamentList
			});
		}

		[HttpPost]
		public async Task<JsonResult> AddEdit(AddEditRequestModel model)
		{
			var response = new AddEditResponseModel();
			if (_sessionManager.GetSession(model.SessionId) == null)
			{
				response.IsError = true;
				response.Message = "Sesja wygasła. Zaloguj się ponownie";
				return new JsonResult(response);
			}

			response.IsError = false;
			response.Message = "Operacja zakończona sukcesem";
			var tournament = new Tournament
			{
				Game = model.Game,
				Name = model.Name,
				State = model.State,
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
					toReplace.Game = tournament.Game;
					toReplace.Name = tournament.Name;
					toReplace.State = tournament.State;
					toReplace.TournamentType = tournament.TournamentType;
					toReplace.TournamentTypeShort = tournament.TournamentTypeShort;
					await _db.SaveChangesAsync();
				}
				else
				{
					response.IsError = true;
					response.Message = "Turniej nie istnieje";
				}
			}

			return new JsonResult(response);
		}
	}
	
	public record TournamentRequestModel
	{
		[FromQuery(Name = "State")]
		public string? State { get; set; }
	}

	public record AddEditRequestModel
	{
		public string SessionId { get; set; }
		public int Id { get; set; }
		public string State { get; set; }
		public string Game { get; set; }
		public string Name { get; set; }
		public string TournamentType { get; set; }
		public string TournamentTypeShort { get; set; }
	}

	public record AddEditResponseModel
	{
		public bool IsError { get; set; }
		public string Message { get; set; }
	}
}
