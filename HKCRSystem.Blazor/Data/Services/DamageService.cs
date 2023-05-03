using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class DamageService
    {

        private readonly HttpClient _httpClient;

        private string baseUrl = "https://localhost:7284/";
        public DamageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> CreateDamage(string requestId, string description, string damageDate, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/add/damage");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(requestId), "requestId");
            content.Add(new StringContent(description), "description");
            content.Add(new StringContent(damageDate), "damageDate");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var response = await _httpClient.GetAsync("https://localhost:7284/api/get/damage");

            return new ResponseDTO { Status = "Success", Message = result };
        }

        public async Task<ResponseDTO> UpdateDamage(string damageId, string price, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7096/api/update/damage/{damageId}");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(price), "price");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return new ResponseDTO { Status = "Success", Message = result };
            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<List<DamageData>>(result);
            return rr;
        }
    }
}
