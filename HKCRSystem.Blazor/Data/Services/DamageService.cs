using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class DamageService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7284/";
        public DamageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DamageData>?> GetDamageAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7284/api/get/damage");

            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<List<DamageData>>(result);
            return rr;
        }
    }
}
