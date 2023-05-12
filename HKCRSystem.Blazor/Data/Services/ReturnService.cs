using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace HKCRSystem.Blazor.Data.Services
{
    public class ReturnService
    {
        private readonly HttpClient _httpClient;

        public ReturnService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReturnData>?> GetReturnAsync(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7096/api/get/return-car");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return null;
            }

            var rr = JsonConvert.DeserializeObject<List<ReturnData>>(result);
            return rr;
        }

        public async Task<ResponseDTO> CreateRetun(Guid requestId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/return/car");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var data = new
            {
                RequestId = requestId
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