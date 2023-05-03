using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json.Linq;

namespace HKCRSystem.Blazor.Data.Services
{
    public class BillingService
    {

        private readonly HttpClient _httpClient;
        public BillingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> UpdateBilling(string billingId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7096/api/update/billing/{billingId}");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

        }

    }
}
