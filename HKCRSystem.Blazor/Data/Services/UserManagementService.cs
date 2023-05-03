using HKCRSystem.Blazor.Data.DTO;
using System.Net.Sockets;

namespace HKCRSystem.Blazor.Data.Services
{
    public class UserManagementService
    {
        private readonly HttpClient _httpClient;

        public UserManagementService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseDTO> AddStaff(string firstname, string lastname, string email, string phoneNumber, string address, string role, string password, string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/user/add/staff");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(role), "role");
            content.Add(new StringContent(password), "password");
            content.Add(new StringContent(address), "address");
            content.Add(new StringContent(phoneNumber), "phoneNumber");
            content.Add(new StringContent(email), "email");
            content.Add(new StringContent(lastname), "lastName");
            content.Add(new StringContent(firstname), "firstName");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return new ResponseDTO { Status = "Success", Message = result };

        }

        public async Task<ResponseDTO> UpdateStaff(string id, string firstname, string lastname, string email, string phoneNumber, string address, string role, string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7096/api/user/update/staff");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(role), "role");
            content.Add(new StringContent(id), "id");
            content.Add(new StringContent(address), "address");
            content.Add(new StringContent(phoneNumber), "phoneNumber");
            content.Add(new StringContent(email), "email");
            content.Add(new StringContent(lastname), "lastName");
            content.Add(new StringContent(firstname), "firstName");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return new ResponseDTO { Status = "Success", Message = result };
        }

        public async Task<ResponseDTO> DeleteStaff(string id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7096/api/user/delete/staff/{id}");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
        
            return new ResponseDTO { Status = "Success", Message = result };
        }
    }
}
