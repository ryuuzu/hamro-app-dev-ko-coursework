using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class OfferService
    {
        private readonly HttpClient _httpClient;

        public OfferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        {
            
        }

        public async Task<ResponseDTO> UpdateOffer(string id, string name, string message, string startDate, string endDate, string type, string discount)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7096/api/update/offer");
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJkY2ZmYzQ3MS0yOGIyLTRlODgtYWQwOC1kNDg2Yzk3OTkwZWQiLCJlbWFpbCI6ImhrY3JfYWRtaW5AZ21haWwuY29tIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNjgzMDQxODc3LCJleHAiOjE2ODMwODUwNzcsImlhdCI6MTY4MzA0MTg3NywiaXNzIjoiaGtjcnMiLCJhdWQiOiJoa2Nycy1hcHAifQ.BgBHjhxg5IeaikHNbxKElvOr_K-bXdD_tvjPR8eyIuY");
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

        public async Task<ResponseDTO> DeleteOffer(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7096/api/delete/offer/{id}");
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJkY2ZmYzQ3MS0yOGIyLTRlODgtYWQwOC1kNDg2Yzk3OTkwZWQiLCJlbWFpbCI6ImhrY3JfYWRtaW5AZ21haWwuY29tIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNjgzMDQxODc3LCJleHAiOjE2ODMwODUwNzcsImlhdCI6MTY4MzA0MTg3NywiaXNzIjoiaGtjcnMiLCJhdWQiOiJoa2Nycy1hcHAifQ.BgBHjhxg5IeaikHNbxKElvOr_K-bXdD_tvjPR8eyIuY");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return new ResponseDTO { Status = "Success", Message = result };
        }
    }
}
