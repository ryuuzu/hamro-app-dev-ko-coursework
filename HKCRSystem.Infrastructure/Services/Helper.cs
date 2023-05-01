using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace HKCRSystem.Infrastructure.Services
{
    public class Helper : IHelper
    {
        public string GetIdFromToken(HttpContext context)
        {
            // Get the JWT token from the Authorization header
            string authHeader = context.Request.Headers["Authorization"];
            string token = authHeader?.Split(' ')[1];

            // Decode the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Extract the id from the JWT token's payload
            string? id = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            return id;
        }

        public ResponseDTO ValidateFile(IFormFile file)
        {
            //system supported file format
            List<string> supportTypes = new List<string> { ".pdf", ".png" };

            //gets the extension of file
            var path = Path.GetExtension(file.FileName);
            //checks if file matches with supported files
            if (!supportTypes.Contains(path))
                return new ResponseDTO { Status = "Error", Message = "File should be of pdf or png format!" };

            //checks if file size is greater than 1.5MB
            if (file.Length > 1.5 * 1024 * 1024)
                return new ResponseDTO { Status = "Error", Message = "File size should not exceed 1.5 MB!" };
            return new ResponseDTO { Status = "Success", Message = "File is valid." };
        }
    }
}
