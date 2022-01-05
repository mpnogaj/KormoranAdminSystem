using KormoranAdminSystemRevamped.Contexts;
using KormoranAdminSystemRevamped.Models;
using KormoranAdminSystemRevamped.Models.Responses;
using KormoranAdminSystemRevamped.Properties;
using KormoranAdminSystemRevamped.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
		public async Task<JsonResult> GetTournaments([FromQuery] TournamentRequestModel model)
		{
			try
			{
				var tournamentList = await _db.Tournaments
								.Include(x => x.Teams)
								.Include(x => x.Matches)
								.ToListAsync();
				if (model.StateId != null)
				{
					tournamentList = tournamentList.Where(x => x.State.Id == model.StateId).ToList();
				}

				return new JsonResult(new CollectionResponse<Tournament>()
				{
					Message = Resources.operationSuccessfull,
					Error = false,
					Collection = tournamentList
				});
			}
			catch
			{
				return new JsonResult(new CollectionResponse<Tournament>()
				{
					Message = Resources.serverError,
					Error = true,
					Collection = null
				});
			}
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
				return new JsonResult(new CollectionResponse<Match>
				{
					Error = true,
					Collection = null
				});
			}

			return new JsonResult(new CollectionResponse<Match>
			{
				Error = false,
				Collection = tournament.Matches.ToList()
			});
		}

		[HttpPost]
		public async Task<JsonResult> AddEdit(AddEditRequestModel model)
		{
			var response = new BasicResponse();
			if (_sessionManager.GetSession(model.SessionId) == null)
			{
				response.Error = true;
				response.Message = "Sesja wygasła. Zaloguj się ponownie";
				return new JsonResult(response);
			}

			response.Error = false;
			response.Message = Resources.operationSuccessfull;
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

		[HttpGet]
		public async Task<JsonResult> GetStates()
		{
			try
			{
				var states = await _db.States.ToListAsync();
				return new JsonResult(new CollectionResponse<State>
				{
					Message = Resources.operationSuccessfull,
					Error = false,
					Collection = states
				});
			}
			catch
			{
				return new JsonResult(new CollectionResponse<State>
				{
					Message = Resources.serverError,
					Error = true,
					Collection = null
				});
			}
		}

		[HttpGet]
		public async Task<JsonResult> GetDisciplines()
		{
			try
			{
				var disciplines = await _db.Disciplines.ToListAsync();
				return new JsonResult(new CollectionResponse<Discipline>
				{
					Message = Resources.operationSuccessfull,
					Error = false,
					Collection = disciplines
				});
			}
			catch
			{
				return new JsonResult(new CollectionResponse<Discipline>
				{
					Message = Resources.serverError,
					Error = true,
					Collection = null
				});
			}
		}
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

	public record TournamentRequestModel
	{
		[FromQuery(Name = "stateId")]
		public int? StateId { get; set;}
	}
}
