using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class ReturnService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7284/";

        public ReturnService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReturnData>?> GetReturnAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7284/api/get/return");

            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<List<ReturnData>>(result);
            return rr;
        }

        public async Task<ResponseDTO> CreateRetun(string requestId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/return/car");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(""), "requestId");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return new ResponseDTO { Status = "Success", Message = result };
            // var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/return/car");
            // request.Headers.Add("Authorization", $"Bearer {token}");
            // var content = new MultipartFormDataContent();
            // content.Add(new StringContent(""), "requestId");
            // request.Content = content;
            // var response = await _httpClient.SendAsync(request);
            // response.EnsureSuccessStatusCode();
            // var result = await response.Content.ReadAsStringAsync();
            // var rr = JsonConvert.DeserializeObject<List<ReturnData>>(result);
            // return rr;
        }
    }
}