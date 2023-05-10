using Blazored.LocalStorage;
using System.Text;

namespace HKCRSystem.Blazor.Data.Services
{
    public class Authorize
    {
        private readonly ILocalStorageService _localStorage;

        public Authorize(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<int> CheckToken()
        {
            var token = await _localStorage.GetItemAsync<string>("access");
            if (token == null)
            {
                return 0;
            }
            return 1;
        }

        public async Task<string> GetToken()
        {
            var token = await _localStorage.GetItemAsync<string>("access");
            if (token == null)
            {
                return null;
            }
            return token;
        }

        public async Task<string> GetUser()
        {
            var user = await _localStorage.GetItemAsync<string>("user");
            if (user == null)
            {
                return "";
            }
            return Decoder(user);
        }

        public async Task<string> GetRole()
        {
            var role = await _localStorage.GetItemAsync<string>("role");
            if (role == null)
            {
                return "";
            }
            return Decoder(role);
        }

        public string Encoder(string value)
        {
            // Encode the string to bytes using UTF-8 encoding
            byte[] encodedBytes = Encoding.UTF8.GetBytes(value);

            // Convert the bytes to a Base64-encoded string
            string base64String = Convert.ToBase64String(encodedBytes);

            return base64String;
        }

        public string Decoder(string value)
        {
            // Convert the Base64-encoded string to bytes
            byte[] encodedBytes = Convert.FromBase64String(value);

            // Decode the bytes to a string using UTF-8 encoding
            string decodedString = Encoding.UTF8.GetString(encodedBytes);

            return decodedString;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("access");
            await _localStorage.RemoveItemAsync("user");
            await _localStorage.RemoveItemAsync("role");
        }
    }
}
