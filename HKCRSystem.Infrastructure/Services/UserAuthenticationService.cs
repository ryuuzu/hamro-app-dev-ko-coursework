using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Infrastructure.Services
{
    public class UserAuthenticationService : IUserAuthentication
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserAuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResponseDTO> Register(UserRegisterRequestDTO model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new ResponseDTO { Status = "Error", Message = "User already exists!" };

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return
                    new ResponseDTO
                    { Status = "Error", Message = "User creation failed! Please check user details and try again." };

            return new ResponseDTO { Status = "Success", Message = "User created successfully!" };
        }
    }
}
