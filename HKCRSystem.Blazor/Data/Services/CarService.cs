using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class CarService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7096/";
        public CarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CarData>?> GetCarAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7096/api/get/car");

            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<List<CarData>>(result);
            return rr;
        }

    }
}
