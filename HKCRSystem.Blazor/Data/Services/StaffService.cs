using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net;

namespace HKCRSystem.Blazor.Data.Services
{
    public class StaffService
    {
        private readonly HttpClient _httpClient;
        public StaffService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StaffData>?> GetStaffAsync(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7096/api/user/get/staff");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return null;
            }

            var rr = JsonConvert.DeserializeObject<List<StaffData>>(result);
            return rr;
        }
    }
}
