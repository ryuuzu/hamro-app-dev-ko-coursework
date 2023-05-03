using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class DamageCustomerService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7284/";
        public DamageCustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DamageDTO>?> GetDamageCustomerAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7284/api/get/damage");

            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<List<DamageDTO>>(result);
            return rr;
        }
    }
}
