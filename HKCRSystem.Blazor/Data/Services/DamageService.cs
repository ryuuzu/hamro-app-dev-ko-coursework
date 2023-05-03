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

        public async Task<ResponseDTO> CreateDamage(string requestId, string description, string damageDate)
        public async Task<List<DamageData>?> GetDamageAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/add/damage");
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJkY2ZmYzQ3MS0yOGIyLTRlODgtYWQwOC1kNDg2Yzk3OTkwZWQiLCJlbWFpbCI6ImhrY3JfYWRtaW5AZ21haWwuY29tIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNjgzMDQxODc3LCJleHAiOjE2ODMwODUwNzcsImlhdCI6MTY4MzA0MTg3NywiaXNzIjoiaGtjcnMiLCJhdWQiOiJoa2Nycy1hcHAifQ.BgBHjhxg5IeaikHNbxKElvOr_K-bXdD_tvjPR8eyIuY");
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

        public async Task<ResponseDTO> UpdateDamage(string damageId, string price)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7096/api/update/damage/{damageId}");
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJkY2ZmYzQ3MS0yOGIyLTRlODgtYWQwOC1kNDg2Yzk3OTkwZWQiLCJlbWFpbCI6ImhrY3JfYWRtaW5AZ21haWwuY29tIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNjgzMDQxODc3LCJleHAiOjE2ODMwODUwNzcsImlhdCI6MTY4MzA0MTg3NywiaXNzIjoiaGtjcnMiLCJhdWQiOiJoa2Nycy1hcHAifQ.BgBHjhxg5IeaikHNbxKElvOr_K-bXdD_tvjPR8eyIuY");
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
