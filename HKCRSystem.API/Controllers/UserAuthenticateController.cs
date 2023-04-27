using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;

namespace HKCRSystem.API.Controllers
{
    [ApiController]
    public class UserAuthenticateController : ControllerBase
    {
        private readonly IUserAuthentication _authenticationManager;

        public UserAuthenticateController(IUserAuthentication authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpPost]
        [Route("/api/auth/register")]
        public async Task<ResponseDTO> Register([FromBody] UserRegisterRequestDTO model)
        {
            var result = await _authenticationManager.Register(model);
            return result;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/auth/login")]
        public async Task<ResponseDTO> Login([FromBody] UserLoginRequestDTO user)
        {
            var result = await _authenticationManager.Login(user);
            return result;
        }

        [HttpPost]
        [Authorize]
        [Route("/api/auth/change/password")]
        public async Task<ResponseDTO> PasswordChange([FromBody] ChangePasswordDTO model)
        {
            // Get the JWT token from the Authorization header
            string authHeader = HttpContext.Request.Headers["Authorization"];
            string token = authHeader?.Split(' ')[1];

            // Decode the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Extract the email address from the JWT token's payload
            string? email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            var result = await _authenticationManager.PasswordChange(email, model);
            return result;
        }

        [HttpPost]
        [Route("/api/auth/forgot/password")]
        public async Task ForgotPassword([FromBody] ResetPasswordDTO model)
        {
            await _authenticationManager.ForgotPassword(model);
        }

        [HttpPost]
        [Route("/api/auth/reset/password")]
        public async Task ResetPassword([FromBody] ResetPasswordConfirmDTO model)
        {
            await _authenticationManager.ResetPassword(model);
        }
    }
}
