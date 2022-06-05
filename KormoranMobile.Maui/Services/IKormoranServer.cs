using KormoranShared.Models;
using KormoranShared.Models.Requests;
using KormoranShared.Models.Requests.Matches;
using KormoranShared.Models.Responses;
using Refit;

namespace KormoranMobile.Maui.Services
{
	internal interface IKormoranServer
	{
		#region Test
		[Get("/Ping")]
		Task<string> PingTest();
		#endregion

		#region User
		[Post("/User/Login")]
		public Task<AuthenticateResponse> Authenticate([Body] AuthenticateRequest request);
		#endregion

		#region Touranaments
		[Get("/tournaments/GetTournaments")]
		public Task<CollectionResponse<Tournament>> GetTournaments();
		#endregion

		#region Matches

		[Get("/matches/GetMatches")]
		public Task<CollectionResponse<Match>> GetMatches([Query] int tournamentId);

		[Post("/matches/UpdateScore")]
		public Task<BasicResponse> UpdateScore([Body] UpdateScoreRequestModel updateScoreRequestModel);
		#endregion
	}
}
