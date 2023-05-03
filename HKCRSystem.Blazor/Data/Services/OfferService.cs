using HKCRSystem.Blazor.Data.DTO;
using System.Globalization;

namespace HKCRSystem.Blazor.Data.Services
{
    public class OfferService
    {
        private readonly HttpClient _httpClient;

        public OfferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> AddOffer(string name, string message, string startDate, string endDate, string type, string discount, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/add/offer");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(name), "name");
            content.Add(new StringContent(message), "message");
            content.Add(new StringContent(startDate), "startDate");
            content.Add(new StringContent(endDate), "endDate");
            content.Add(new StringContent(type), "type");
            content.Add(new StringContent(discount), "discountPercent");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            
            return new ResponseDTO { Status = "Success", Message = result };
        }

        public async Task<ResponseDTO> UpdateOffer(string id, string name, string message, string startDate, string endDate, string type, string discount, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7096/api/update/offer");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(id), "id");
            content.Add(new StringContent(name), "name");
            content.Add(new StringContent(message), "message");
            content.Add(new StringContent(startDate), "startDate");
            content.Add(new StringContent(endDate), "endDate");
            content.Add(new StringContent(type), "type");
            content.Add(new StringContent(discount), "discountPercent");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return new ResponseDTO { Status = "Success", Message = result };
        }

        public async Task<ResponseDTO> DeleteOffer(string id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7096/api/delete/offer/{id}");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return new ResponseDTO { Status = "Success", Message = result };
        }
    }
}
