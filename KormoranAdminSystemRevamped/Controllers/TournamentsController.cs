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
				var tournamentList = await _db.Tournaments.ToListAsync();
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
		public async Task<JsonResult> GetAllTournamentData([FromQuery] int id)
		{	
			try
			{
				var tournament = await _db.Tournaments
					.FirstAsync(x => x.Id == id);
				var matches = await _db.Matches
					.Where(x => x.TournamentId == id)
					.ToListAsync();
				var teams = await _db.Teams
					.Where(x => x.TournamentId == id)
					.ToListAsync();
				var states = await _db.States
					.ToListAsync();
				var disciplines = await _db.Disciplines
					.ToListAsync();

				return new JsonResult(new GetFullTournamentDataResponseModel
				{
					Error = false,
					Tournament = tournament,
					Message = Resources.operationSuccessfull,
					Matches = matches,
					Teams = teams,
					States = states,
					Disciplines = disciplines
				});
			}
			catch (Exception ex)
			{
				return new JsonResult(new GetFullTournamentDataResponseModel
				{
					Error = true,
					Message = ex.Message
				});
			}
		}

		[HttpPost]
		public async Task<JsonResult> UpdateTournament([FromBody] UpdateTournamentRequestModel request)
		{
			try
			{
				var tournament = await _db.Tournaments
					.FirstOrDefaultAsync(x => x.Id == request.TournamentId);

				tournament.Name = request.NewName;
				tournament.StateId = request.NewStateId;
				tournament.DisciplineId = request.NewDisciplineId;

				_db.Tournaments.Update(tournament);
				await _db.SaveChangesAsync();

				return new JsonResult(new BasicResponse
				{
					Error = false,
					Message = Resources.operationSuccessfull
				});
			}
			catch (NullReferenceException)
			{
				return new JsonResult(new BasicResponse()
				{
					Error = true,
					Message = "Turniej nie został znaleziony"
				});
			}
			catch (Exception ex)
			{
				return new JsonResult(new BasicResponse
				{
					Error = true,
					Message = $"Błąd serwera! {ex.Message}"
				});
			}
		}

		[HttpGet]
		public async Task<JsonResult> GetMatches([FromQuery] int id)
		{
			var tournament = await _db.Tournaments
				.Include(x => x.Matches)
				.FirstAsync(x => x.Id == id);

			return tournament == null
				? new JsonResult(new CollectionResponse<Match>
				{
					Error = true,
					Collection = null,
					Message = Resources.serverError
				})
				: new JsonResult(new CollectionResponse<Match>
				{
					Error = false,
					Collection = tournament.Matches.ToList(),
					Message = Resources.operationSuccessfull
				});
		}

		[HttpGet]
		public async Task<JsonResult> GetTeams([FromQuery] int id)
		{
			var tournament = await _db.Tournaments
				.Include(x => x.Teams)
				.FirstOrDefaultAsync();

			return tournament == null
				? new JsonResult(new CollectionResponse<Team>
				{
					Error = true,
					Message = Resources.serverError,
					Collection = null
				})
				: new JsonResult(new CollectionResponse<Team>
				{
					Error = false,
					Message = Resources.operationSuccessfull,
					Collection = tournament.Teams.ToList()
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
		public int? StateId { get; set; }
	}

	public record UpdateTournamentRequestModel
	{
		public int TournamentId { get; set; }
		public string? NewName { get; set; }
		public int NewStateId { get; set; }
		public int NewDisciplineId { get; set; }
	}

	public record GetFullTournamentDataResponseModel : BasicResponse
	{
		public Tournament? Tournament { get; set; }
		public List<Match>? Matches { get; set; }
		public List<Team>? Teams { get; set; }
		public List<State>? States { get; set; }
		public List<Discipline>? Disciplines { get; set; }
	}
}
