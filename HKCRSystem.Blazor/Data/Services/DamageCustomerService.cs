using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;

namespace HKCRSystem.Blazor.Data.Services
{
    public class DamageCustomerService
    {
        private readonly HttpClient _httpClient;
        public DamageCustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DamageDTO>?> GetDamageCustomerAsync(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7096/api/get/damage");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return null;
            }

            var rr = JsonConvert.DeserializeObject<List<DamageDTO>>(result);
            return rr;
        }
    }
}
