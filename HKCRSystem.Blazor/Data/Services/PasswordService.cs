using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;
using System.Text;

namespace HKCRSystem.Blazor.Data.Services
{
    public class PasswordService
    {
        private readonly HttpClient _httpClient;

        public PasswordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> ChangePassword(string currentPassword, string newPassword, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/auth/change/password");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var json = new
            {
                CurrentPassword = currentPassword,
                Password = newPassword
            };

            // Convert the JSON object to a string
            var jsonContent = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");

            request.Content = jsonContent;
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
