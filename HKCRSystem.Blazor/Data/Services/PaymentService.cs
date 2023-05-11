using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class PaymentService
    {

        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> CreatePayment(Guid billingId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7096/api/payment/{billingId}");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await _httpClient.SendAsync(request);
            if(response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return null;
            }
            var result = await response.Content.ReadAsStringAsync();

            var resulta = JsonConvert.DeserializeObject<ResponseDTO>(result);
            return resulta;
        }
    }
}
