using Refit;
using KormoranShared.Models.Requests;
using KormoranShared.Models.Responses;

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
    }
}
