using HKCRSystem.Blazor.Data.DTO;
using System.Text.Json;
using System.Text;

namespace HKCRSystem.Blazor.Data.Services
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7190/";
        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> UserLogin(string email, string password)
        {
            var content = new StringContent(
        JsonSerializer.Serialize(new { email, password }),
        Encoding.UTF8,
        "application/json"
    );
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/auth/login");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return new ResponseDTO { Status = "Success", Message = result };
        }
    }
}
