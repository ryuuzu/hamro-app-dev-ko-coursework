using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace HKCRSystem.Blazor.Data.Services
{
    public class RequestService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7096/";
        public RequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RequestData>?> GetRequestAsync(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7096/api/get/request");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return null;
            }

            var rr = JsonConvert.DeserializeObject<List<RequestData>>(result);
            return rr;
        }

        public async Task<ResponseDTO> AcceptRequest(string token, Guid requestId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7096/api/accept/request/" + requestId.ToString());
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return null;
            }

            var rr = JsonConvert.DeserializeObject<ResponseDTO>(result);
            return rr;
        }
        public async Task<ResponseDTO> DenyRequest(string token, Guid requestId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7096/api/deny/request/" + requestId.ToString());
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return null;
            }

            var rr = JsonConvert.DeserializeObject<ResponseDTO>(result);
            return rr;
        }
    }
}
