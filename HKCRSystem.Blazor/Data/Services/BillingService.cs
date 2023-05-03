using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class BillingService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7284/";
        public BillingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BillingData>?> GetBillingAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7284/api/get/billing");

            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<List<BillingData>>(result);
            return rr;
        }

    }
}