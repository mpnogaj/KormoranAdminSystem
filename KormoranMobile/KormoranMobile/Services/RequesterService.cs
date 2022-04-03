using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KormoranMobile.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly:Dependency(typeof(RequesterService))]
namespace KormoranMobile.Services
{
    public class RequesterService : IRequesterService
    {
        const string JSON_TYPE = "application/json";
        //http://127.0.0.1/ -> localhost
        const string BASE_URL = "http://127.0.0.1";
        private HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BASE_URL)
        };

        public async Task<T> SendPost<T>(string endpoint, object data)
        {
            string json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, JSON_TYPE);
            var response = await _httpClient.PostAsync(endpoint, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new ConnectionException();
            }
            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringResponse);
        }

        public async Task<T> SendGet<T>(string endpoint, Dictionary<string, string> urlParams)
        {
            var urlParamsString = await ParamsToStringAsync(urlParams);
            var response = await _httpClient.GetAsync($"{endpoint}?{urlParamsString}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ConnectionException();
            }
            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringResponse);
        }

        private static async Task<string> ParamsToStringAsync(Dictionary<string, string> urlParams)
        {
            using (HttpContent content = new FormUrlEncodedContent(urlParams))
            {
                return await content.ReadAsStringAsync();
            }
        }
    }

    public class ConnectionException : Exception
    {
        public override string Message { get => "HTTP status not 200"; }
    }
}
