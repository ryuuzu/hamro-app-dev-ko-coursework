using HKCRSystem.Blazor.Data.DTO;

namespace HKCRSystem.Blazor.Data.Services
{
    public class ReturnService
    {
        private readonly HttpClient _httpClient;

        public ReturnService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> CreateRetun(string requestId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7096/api/return/car");
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJkY2ZmYzQ3MS0yOGIyLTRlODgtYWQwOC1kNDg2Yzk3OTkwZWQiLCJlbWFpbCI6ImhrY3JfYWRtaW5AZ21haWwuY29tIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNjgzMDQxODc3LCJleHAiOjE2ODMwODUwNzcsImlhdCI6MTY4MzA0MTg3NywiaXNzIjoiaGtjcnMiLCJhdWQiOiJoa2Nycy1hcHAifQ.BgBHjhxg5IeaikHNbxKElvOr_K-bXdD_tvjPR8eyIuY");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(""), "requestId");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return new ResponseDTO { Status = "Success", Message = result };
        }
    }
}
