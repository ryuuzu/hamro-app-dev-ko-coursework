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

        public async Task<ResponseDTO> RequestCar()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/add/request");
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJkY2ZmYzQ3MS0yOGIyLTRlODgtYWQwOC1kNDg2Yzk3OTkwZWQiLCJlbWFpbCI6ImhrY3JfYWRtaW5AZ21haWwuY29tIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNjgzMDQxODc3LCJleHAiOjE2ODMwODUwNzcsImlhdCI6MTY4MzA0MTg3NywiaXNzIjoiaGtjcnMiLCJhdWQiOiJoa2Nycy1hcHAifQ.BgBHjhxg5IeaikHNbxKElvOr_K-bXdD_tvjPR8eyIuY");
            var content = new StringContent("{\r\n  \"requestedCarId\": \"18393d86-59fd-4d81-a527-709f2a85112f\",\r\n  \"startDate\": \"2023-05-01T18:35:49.138Z\",\r\n  \"endDate\": \"2023-05-10T18:35:49.138Z\"\r\n}", null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return new ResponseDTO { Status = "Success", Message = result };
        }
    }
}
