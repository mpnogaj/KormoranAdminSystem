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
		public async Task<JsonResult> GetTournaments([FromQuery] int? id)
		{
			try
			{
				var tournamentList = await _db.Tournaments
					.Include(x => x.Teams)
					.Include(x => x.Matches)
					.Include(x => x.Discipline)
					.Include(x => x.State)
					.OrderBy(x => x.Id)
					.ToListAsync();
				if (id.HasValue)
				{
					var tournament = tournamentList.FirstOrDefault(x => x.Id == id.Value);
					if (tournament != null)
					{
						return new JsonResult(new SingleItemResponse<Tournament>
						{
							Data = tournament,
							Message = Resources.operationSuccessfull,
							Error = false
						});
					}
					return new JsonResult(new SingleItemResponse<Tournament>
					{
						Data = null,
						Message = Resources.serverError,
						Error = true
					});
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

		[HttpPost]
		public async Task<JsonResult> UpdateTournamentBasic([FromBody] UpdateTournamentRequestModel request)
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

		[HttpPost]
		public async Task<JsonResult> UpdateTournament([FromBody] TournamentFullUpdateRequestModel request)
		{
			try
			{
				//_db.Teams.UpdateRange(request.Teams);
				//await _db.SaveChangesAsync();
				var matchesToAdd = new List<Match>();
				var matchesToUpdate = new List<Match>();
				foreach(var match in request.Matches)
				{
					if(match.State == null || match.Team1 == null || match.Team2 == null)
					{
						throw new ArgumentNullException(nameof(match));
					}
					match.StateId = match.State.Id;
					match.Team1Id = match.Team1.Id;
					match.Team2Id = match.Team2.Id;
					match.TournamentId = request.TournamentId;

					//clear unwanted objects
					match.State = null;
					match.Team1 = null;
					match.Team2 = null;
					//new match
					if(match.MatchId > 100000)
					{
						match.MatchId = 0;
						_db.Add(match);
						await _db.SaveChangesAsync();
					}
					else
					{
						matchesToUpdate.Add(match);
					}
				}
				_db.Matches.UpdateRange(matchesToUpdate);
				await _db.SaveChangesAsync();
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


		[HttpPost]
		public async Task<JsonResult> CreateTournament(CreateTournamentRequestModel model)
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
				State = _db.States.FirstOrDefault(x => x.Id == model.StateId)
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

	public record CreateTournamentRequestModel
	{
		public string SessionId { get; set; }
		public int Id { get; set; }
		public int StateId { get; set; }
		public int DisciplineId { get; set; }
		public string Name { get; set; }
		public string TournamentType { get; set; }
		public string TournamentTypeShort { get; set; }
	}

	public record UpdateTournamentRequestModel
	{
		public int TournamentId { get; set; }
		public string? NewName { get; set; }
		public int NewStateId { get; set; }
		public int NewDisciplineId { get; set; }
	}

	public record TournamentFullUpdateRequestModel : UpdateTournamentRequestModel
	{
		public List<Team> Teams { get; set; }
		public List<Match> Matches { get; set; }
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
