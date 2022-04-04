using Refit;
using System.Threading.Tasks;

namespace KormoranMobile.Services
{
    public interface IKormoranServer
    { 
        [Get("/")]
        Task<string> PingTest(); 
    }
}
