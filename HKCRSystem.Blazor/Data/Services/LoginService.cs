using HKCRSystem.Blazor.Data.DTO;
using System.Text;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> UserLogin(string email, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/auth/login");
            var json = new
            {
                email,
                password
            };

            // Convert the JSON object to a string
            var jsonContent = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");

            request.Content = jsonContent;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            var resulta = JsonConvert.DeserializeObject<ResponseDTO>(result);

            return resulta;
        }
    }
}
