using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;
using System.Text;

namespace HKCRSystem.Blazor.Data.Services
{
    public class CarRequestService
    {
        private readonly HttpClient _httpClient;
        
        public CarRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> RequestCar(DateTime? startDate, DateTime? endDate, Guid? carId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/add/request");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var requestData = new
            {
                StartDate = startDate,
                EndDate = endDate,
                RequestedCarId = carId
            };

            var json = JsonConvert.SerializeObject(requestData);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            var result = await response.Content.ReadAsStringAsync();
            var resulta = JsonConvert.DeserializeObject<ResponseDTO>(result);
            return resulta;
        }

        public async Task<ResponseDTO> CancelRequest(string requestId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7096/api/cancel/request/{requestId}");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return new ResponseDTO { Status = "Success", Message = result };
        }

        public async Task<ResponseDTO> ApproveRequest(string requestId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7096/api/accept/request/{requestId}");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return new ResponseDTO { Status = "Success", Message = result };
        }

        public async Task<ResponseDTO> DenyRequest(string requestId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7096/api/deny/request/{requestId}");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return new ResponseDTO { Status = "Success", Message = result };
        }
    }
}
