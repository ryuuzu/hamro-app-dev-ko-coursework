using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Infrastructure.Services
{
    public class UserAuthenticationService : IUserAuthentication
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserAuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<ResponseDTO> Register(UserRegisterRequestDTO model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return new ResponseDTO { Status = "Error", Message = "User already exists!" };

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.FirstName.ToLower(),
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            //saving user as customer
            await _userManager.AddToRoleAsync(user, "Customer");


            if (!result.Succeeded)
                return
                    new ResponseDTO
                    { Status = "Error", Message = "User creation failed! Please check user details and try again." };

            return new ResponseDTO { Status = "Success", Message = "User created successfully!" };
        }

        public async Task<ResponseDTO> Login(UserLoginRequestDTO model)
        {
            //finds user by email
            var user = await _userManager.FindByEmailAsync(model.Email);

            //returns error message if user not found
            if (user == null)
            {
                return new ResponseDTO()
                {
                    Message = "Invalid email or password!",
                    Status = "Error"
                };
            }

            //validates email and password
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            //returns error if invalid
            if (!result.Succeeded)
            {
                return new ResponseDTO()
                {
                    Message = "Invalid email or password!",
                    Status = "Error"
                };
            }

            //gets the user role
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            //retruns success message with token
            return new ResponseDTO { Status = "Success", Message = _tokenService.GenerateToken(user, role) };

        }
    }
}
