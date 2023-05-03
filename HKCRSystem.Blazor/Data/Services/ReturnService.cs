using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services
{
    public class ReturnService
    {
        private readonly HttpClient _httpClient;

            private readonly HttpClient _httpClient;
            private string baseUrl = "https://localhost:7284/";
        public ReturnService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> CreateRetun(string requestId)
        {

        }

    }
}
