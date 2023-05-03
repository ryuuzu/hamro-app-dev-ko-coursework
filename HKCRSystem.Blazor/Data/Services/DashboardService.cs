using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class DashboardService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7284/";

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DashboardDTO>?> GetDashboardAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7284/api/dashboard/");

            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<List<DashboardDTO>?>(result);
            return rr;
        }
    }
}