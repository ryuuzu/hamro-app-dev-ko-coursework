using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class RequestService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7284/";
        public RequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RequestData>?> GetRequestAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7284/api/get/request");

            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<List<RequestData>>(result);
            return rr;
        }

    }
}
