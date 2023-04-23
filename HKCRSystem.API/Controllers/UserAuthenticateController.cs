using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
