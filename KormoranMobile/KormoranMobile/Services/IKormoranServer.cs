using KormoranShared.Models;
using KormoranShared.Models.Requests;
using KormoranShared.Models.Requests.Matches;
using KormoranShared.Models.Responses;
using Refit;
using System.Threading.Tasks;

namespace KormoranMobile.Services
{
    public interface IKormoranServer
    {
        #region Test methods

        [Get("/Ping")]
        Task<string> PingTest();

        #endregion Test methods

        [Post("/")]
        Task<AuthenticateResponse> Login([Body] AuthenticateRequest request);

        [Post("/matches/IncrementScore")]
        Task<BasicResponse> IncrementScore([Body] IncrementScoreRequestModel request);

        [Get("/tournaments/GetTournaments")]
        Task<CollectionResponse<Tournament>> GetTournaments();

        [Get("/matches/GetMatches")]
        Task<CollectionResponse<Match>> GetMatches(int tournamentId);
    }
}