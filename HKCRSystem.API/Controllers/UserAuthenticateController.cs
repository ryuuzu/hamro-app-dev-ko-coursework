using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace HKCRSystem.API.Controllers
{
    [ApiController]
    public class UserAuthenticateController : ControllerBase
    {
        private readonly IUserAuthentication _authenticationManager;
        private readonly IHelper _helper;

        public UserAuthenticateController(IUserAuthentication authenticationManager, IHelper helper)
        {
            _authenticationManager = authenticationManager;
            _helper = helper;
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
            //gets the id from the request header
            string id = _helper.GetIdFromToken(HttpContext);
            var result = await _authenticationManager.PasswordChange(id, model);
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
