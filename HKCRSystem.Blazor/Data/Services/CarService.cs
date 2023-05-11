using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;
using System.Text;

namespace HKCRSystem.Blazor.Data.Services
{
    public class CarService
    {
        private readonly HttpClient _httpClient;
        public CarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CarData>?> GetCarAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7096/api/get/car");

            var result = response.Content.ReadAsStringAsync().Result;
            var rr = JsonConvert.DeserializeObject<List<CarData>>(result);
            return rr;
        }

        public async Task<ResponseDTO> CreateCar(string company, string model, double price, string status, string token)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/add/car");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var data = new
            {
                Company = company,
                Model = model,
                Price = price,
                Status = status
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

        public async Task<ResponseDTO> UpdateCar(Guid? id, string company, string model, double price, string status, bool available, string token)
        {

            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7096/api/update/car");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var data = new
            {
                Id = id,
                Company = company,
                Model = model,
                Price = price,
                Status = status,
                IsAvailable = available
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

        public async Task<ResponseDTO> DeleteCar(string id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7096/api/delete/car/{id}");
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
