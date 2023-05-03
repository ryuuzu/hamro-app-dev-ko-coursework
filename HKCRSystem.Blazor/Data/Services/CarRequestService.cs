using HKCRSystem.Blazor.Data.DTO;

namespace HKCRSystem.Blazor.Data.Services
{
    public class CarRequestService
    {
        private readonly HttpClient _httpClient;
        
        public CarRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> RequestCar(string startDate, string endDate, string carId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/add/request");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(startDate), "endDate");
            content.Add(new StringContent(endDate), "startDate");
            content.Add(new StringContent(carId), "requestedCarId");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return new ResponseDTO { Status = "Success", Message = result };
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
