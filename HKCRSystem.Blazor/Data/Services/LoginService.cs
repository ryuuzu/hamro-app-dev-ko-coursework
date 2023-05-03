using HKCRSystem.Blazor.Data.DTO;

namespace HKCRSystem.Blazor.Data.Services
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;
        private string baseUrl = "https://localhost:7190/";
        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> UserLogin()
        {

        }
    }
}
