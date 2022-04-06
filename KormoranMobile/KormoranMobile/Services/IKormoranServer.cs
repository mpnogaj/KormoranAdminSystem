using KormoranShared.Models.Responses;
using KormoranShared.Models.Requests.Matches;
using Refit;
using System.Threading.Tasks;

namespace KormoranMobile.Services
{
    public interface IKormoranServer
    { 
        [Get("/Ping")]
        Task<string> PingTest();

        [Post("/matches/IncrementScore")]
        Task<BasicResponse> IncrementScore([Body] IncrementScoreRequestModel request);
    }
}
