using Refit;
using System.Threading.Tasks;
using KormoranShared.Models;
using KormoranShared.Models.Requests.Matches;
using KormoranShared.Models.Responses;

namespace KormoranMobile.Services
{
    public interface IKormoranServer
    { 
        [Get("/Ping")]
        Task<string> PingTest();

        [Post("/matches/IncrementScore")]
        Task<BasicResponse> IncrementScore([Body] IncrementScoreRequestModel request);

        [Get("/tournaments/GetTournaments")]
        Task<CollectionResponse<Tournament>> GetTournaments();

        [Get("/matches/GetMatches")]
        Task<CollectionResponse<Match>> GetMatches(int tournamentId);
    }
}
