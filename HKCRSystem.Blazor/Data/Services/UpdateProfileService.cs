using HKCRSystem.Blazor.Data.DTO;
using Newtonsoft.Json;

namespace HKCRSystem.Blazor.Data.Services;

public class UpdateProfileService
{
    private readonly HttpClient _httpClient;

    public UpdateProfileService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResponseDTO?> UpdateUserProfile(string accessToken, string firstName, string lastName, string email,
        string phoneNumber, string address, string password, Stream? document)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7096/api/user/update/profile");
        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(firstName), "FirstName");
        content.Add(new StringContent(lastName), "LastName");
        content.Add(new StringContent(email), "Email");
        content.Add(new StringContent(phoneNumber), "PhoneNumber");
        content.Add(new StringContent(address), "Address");
        content.Add(new StringContent(password), "Password");
        content.Add(new StreamContent(document), "file");
        request.Content = content;
        var response = await _httpClient.SendAsync(request);
        var result = response.Content.ReadAsStringAsync().Result;
        var rr = JsonConvert.DeserializeObject<ResponseDTO>(result);
        return rr;
    }
}