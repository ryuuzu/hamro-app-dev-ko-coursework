using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Data;
using System.Text;

namespace HKCRSystem.Blazor.Data.Services
{
    public class DamageService
    {
        private readonly HttpClient _httpClient;

        public DamageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> CreateDamage(Guid requestId, string description, DateTime? damageDate,
            string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/add/damage");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var data = new
            {
                RequestId = requestId,
                Description = description,
                DamageDate = damageDate,
            };

            var json = JsonConvert.SerializeObject(data);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return null;
            }
            var result = await response.Content.ReadAsStringAsync();
            var resulta = JsonConvert.DeserializeObject<ResponseDTO>(result);
            return resulta;
        }

        public async Task<List<DamageData>?> GetDamageAsync(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7096/api/get/damage");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return null;
            }

            var rr = JsonConvert.DeserializeObject<List<DamageData>>(result);
            return rr;
        }

        public async Task<ResponseDTO> UpdateDamage(string damageId, double price, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7096/api/update/damage/{damageId}");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var data = new
            {
                Price = price
            }; 
            var json = JsonConvert.SerializeObject(data);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return null;
            }
            var result = await response.Content.ReadAsStringAsync();
            var resulta = JsonConvert.DeserializeObject<ResponseDTO>(result);
            return resulta;
        }
    }
}