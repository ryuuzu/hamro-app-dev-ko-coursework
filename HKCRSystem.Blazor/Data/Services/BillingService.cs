using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;

namespace HKCRSystem.Blazor.Data.Services
{
    public class BillingService
    {
        private readonly HttpClient _httpClient;

        public BillingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SalesData>> GetSalesAsync(string token)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7096/api/get/sales");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return null;
            }

            var rr = JsonConvert.DeserializeObject<List<SalesData>>(result);
            return rr;
        }

        public async Task<ResponseDTO> UpdateBilling(Guid billingId, string token)
        {
            var request =
                new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7096/api/update/billing/{billingId}");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            var rr = JsonConvert.DeserializeObject<ResponseDTO>(result);
            return rr;
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