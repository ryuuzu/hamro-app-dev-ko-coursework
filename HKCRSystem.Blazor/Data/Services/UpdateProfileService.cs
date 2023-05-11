using HKCRSystem.Blazor.Data.DTO;
using Microsoft.AspNetCore.Components.Forms;
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
        string phoneNumber, string address, IBrowserFile? document)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7096/api/user/update/profile");
        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        var content = new MultipartFormDataContent();
        if (firstName != null || firstName.Length < 1)
        {
            content.Add(new StringContent(firstName), "FirstName");
        }
        if (lastName != null || lastName.Length < 1)
        {
            content.Add(new StringContent(lastName), "LastName");
        }
        if (email != null || email.Length < 1)
        {
            content.Add(new StringContent(email), "Email");
        }
        if (phoneNumber != null || phoneNumber.Length < 1)
        {
            content.Add(new StringContent(phoneNumber), "PhoneNumber");
        }
        if (address != null || address.Length < 1)
        {
            content.Add(new StringContent(address), "Address");
        }
        if (document != null)
        {
            var fileContent = new StreamContent(document.OpenReadStream(maxAllowedSize: 1500000));
            content.Add(fileContent, "\"file\"", document.Name);
        }

        //content.Add(new StringContent(password), "Password");

        request.Content = content;
        var response = await _httpClient.SendAsync(request);
        var result = response.Content.ReadAsStringAsync().Result;
        var rr = JsonConvert.DeserializeObject<ResponseDTO>(result);
        return rr;
    }
}