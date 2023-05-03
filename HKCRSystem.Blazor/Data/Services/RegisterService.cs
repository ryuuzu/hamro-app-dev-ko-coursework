using HKCRSystem.Blazor.Data.DTO;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace HKCRSystem.Blazor.Data.Services
{
    public class RegisterService
    {
        private readonly HttpClient _httpClient;

        public RegisterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO?> RegisterUser(string firstName, string lastName, string email,
            string phoneNumber, string address, string password, Stream? document)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/auth/register");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(firstName), "FirstName");
            content.Add(new StringContent(lastName), "LastName");
            content.Add(new StringContent(email), "Email");
            content.Add(new StringContent(phoneNumber), "PhoneNumber");
            content.Add(new StringContent(address), "Address");
            content.Add(new StringContent(password), "Password");
            content.Add(new StreamContent(document), "file");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<ResponseDTO>(result);
            return rr;
        }
    }
}