using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class StaffService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7284/";
        public StaffService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StaffData>?> GetStaffAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7284/api/get/staff");

            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<List<StaffData>>(result);
            return rr;
        }
    }
}
