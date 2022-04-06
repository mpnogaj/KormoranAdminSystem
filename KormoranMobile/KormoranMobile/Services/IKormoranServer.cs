using KormoranMobile.ViewModels;
using KormoranShared.Models.Responses;
using Refit;
using System.Threading.Tasks;

namespace KormoranMobile.Services
{
    public interface IKormoranServer
    { 
        [Get("/Ping")]
        Task<string> PingTest();

        [Post("/matches/IncrementScore")]
        Task<BasicResponse> IncrementScore([Body] RequestModel modelrequest);
    }
}
