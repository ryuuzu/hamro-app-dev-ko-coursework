using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7284/";
        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CustomerData>?> GetCustomerAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7284/api/get/customer");

            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<List<CustomerData>>(result);
            return rr;
        }
    }
}
