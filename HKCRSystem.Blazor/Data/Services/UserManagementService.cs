using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

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

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/user/add/staff");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var data = new
            {
                Role = role,
                Password = password,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                LastName = lastname,
                FirstName = firstname
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

        public async Task<ResponseDTO> UpdateStaff(string id, string firstname, string lastname, string email, string phoneNumber, string address, string role, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7096/api/user/update/staff");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var data = new
            {
                Id = id,
                Role = role,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                LastName = lastname,
                FirstName = firstname
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

        public async Task<ResponseDTO> DeleteStaff(string id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7096/api/user/delete/staff/{id}");
            request.Headers.Add("Authorization", $"Bearer {token}");
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
