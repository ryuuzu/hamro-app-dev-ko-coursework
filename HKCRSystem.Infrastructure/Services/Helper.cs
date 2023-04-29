using HKCRSystem.Application.Common.Interface;
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
    }
}
