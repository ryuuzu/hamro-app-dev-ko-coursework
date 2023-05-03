using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class ReturnService
    {
        
            private readonly HttpClient _httpClient;
            private string baseUrl = "https://localhost:7284/";
            public ReturnService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<List<ReturnData>?> GetReturnAsync()
            {
                var response = await _httpClient.GetAsync("https://localhost:7284/api/get/return");

                var result = response.Content.ReadAsStringAsync().Result;
                var rr = JsonConvert.DeserializeObject<List<ReturnData>>(result);
                return rr;
            }

    }
}
