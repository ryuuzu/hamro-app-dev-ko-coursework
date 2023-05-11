using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class RequestCustomerService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7096/";
        public RequestCustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RequestDTO>?> GetRequestCustomerAsync(string token)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7096/api/get/request");
            request.Headers.Add("Authorization", "Bearer " + token);

            var response = await _httpClient.SendAsync(request);

            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<List<RequestDTO>>(result);
            return rr;
        }

    }
}
