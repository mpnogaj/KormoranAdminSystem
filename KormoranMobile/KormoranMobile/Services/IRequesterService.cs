using System.Collections.Generic;
using System.Threading.Tasks;

namespace KormoranMobile.Services
{
    public interface IRequesterService
    {
        Task<T> SendGet<T>(string endpoint, Dictionary<string, string> urlParams);
        Task<T> SendPost<T>(string address, string endpoint, object data);
    }
}