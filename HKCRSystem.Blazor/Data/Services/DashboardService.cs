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

        public async Task<ResponseDTO> GetDashboardAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7096/api/dashboard/");

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            var rr = JsonConvert.DeserializeObject<ResponseDTO>(result);
            return rr;
        }
    }
}