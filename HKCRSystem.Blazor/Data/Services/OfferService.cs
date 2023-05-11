using HKCRSystem.Blazor.Data.DTO;
using System.Globalization;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using System.Net.Http.Headers;
using System.Net;
using System.Data;

namespace HKCRSystem.Blazor.Data.Services
{
    public class OfferService
    {
        private readonly HttpClient _httpClient;

        public OfferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> AddOffer(string name, string message, DateTime? startDate, DateTime? endDate,
            string type, float discount, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/add/offer");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var data = new
            {
                Name = name,
                Message = message,
                StartDate = startDate,
                EndDate = endDate,
                Type = type,
                Discount = discount,
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

        public async Task<List<OfferData>?> GetOfferAsync(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7096/api/get/offer");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return null;
            }

            var rr = JsonConvert.DeserializeObject<List<OfferData>>(result);
            return rr;
        }

        public async Task<ResponseDTO> UpdateOffer(Guid id, string name, string message, DateTime? startDate,
            DateTime? endDate, string type, float discount, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7096/api/update/offer");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var data = new
            {
                Id = id,
                Name = name,
                Message = message,
                StartDate = startDate,
                EndDate = endDate,
                Type = type,
                DiscountPercent = discount,
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

        public async Task<ResponseDTO> DeleteOffer(string id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7096/api/delete/offer/{id}");
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