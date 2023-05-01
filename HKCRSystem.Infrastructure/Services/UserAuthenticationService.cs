using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using HKCRSystem.Infrastructure.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HKCRSystem.Infrastructure.Services
{
    public class UserAuthenticationService : IUserAuthentication
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IApplicationDBContext _dbContext;
        private readonly IHelper _helper;

        public UserAuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IEmailService emailService, IApplicationDBContext dbContext, IHelper helper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _dbContext = dbContext;
            _helper = helper;
        }

        public async Task<ResponseDTO> Register(UserRegisterRequestDTO model, IFormFile file)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return new ResponseDTO { Status = "Error", Message = "User already exists!" };

            if (file != null)
            {
                // Validate file
                var validationResult = _helper.ValidateFile(file);
                if (validationResult.Status == "Error")
                {
                    return validationResult;
                }
            }

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

            // Check if image is null
            if (file != null)
            {
                var userDetail = await _userManager.FindByEmailAsync(model.Email);
                //saves the file
                await PostAttachment(file, model.Description, model.Type, userDetail.Id);
            }

            if (!result.Succeeded)
                return
                    new ResponseDTO
                    { Status = "Error", Message = "User creation failed! Please check user details and try again." };

            return new ResponseDTO { Status = "Success", Message = "User created successfully!" };
        }

        public async Task PostAttachment(IFormFile file, string description, string type, string id)
        {
            var attachment = new Attachment
            {
                Name = Path.GetFileNameWithoutExtension(file.FileName),
                Description = description,
                Type = type,
                Path = Path.Combine("Assest", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)),
                UserId = id
            };

            using (var stream = new FileStream(attachment.Path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _dbContext.Attachments.Add(attachment);

            await _dbContext.SaveChangesAsync(default(CancellationToken));
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

        public async Task<ResponseDTO> PasswordChange(string id, ChangePasswordDTO model)
        {
            //gets the user by email
            var user = await _userManager.FindByIdAsync(id);

            //validates presence of email
            if (user == null)
            {
                return new ResponseDTO()
                {
                    Message = "Password changed failed!!",
                    Status = "Error"
                };
            }

            //checks if user given current password matches with system saved password
            var check = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (!check)
            {
                return new ResponseDTO()
                {
                    Message = "Current password does not match!",
                    Status = "Error",
                };
            }

            //changes the password
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);

            if (result.Succeeded)
            {
                return new ResponseDTO()
                {
                    Message = "Password changed successfully.",
                    Status = "Success"
                };
            }

            return new ResponseDTO()
            {
                Message = "Password changed failed!!",
                Status = "Error"
            };
        }

        public async Task ForgotPassword(ResetPasswordDTO model)
        {
            //gets the email and validates its presence
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                //generates token for user
                var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                //converting token to url-safe base 64
                var token = ToUrlSafeBase64(passwordResetToken);
                //calls method to send email
                await _emailService.SendForgotPasswordEmailAsync(user.FirstName, model.Email, token);
            }
        }

        public async Task ResetPassword(ResetPasswordConfirmDTO model)
        {
            //gets the email and validates
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                //gets the original token
                var passwordResetToken = FromUrlSafeBase64(model.Token);
                //calls method to reset password
                var result = await _userManager.ResetPasswordAsync(user, passwordResetToken, model.Password);

                //validates the success status of result
                ValidateIdentityResult(result);
            }
        }

        private void ValidateIdentityResult(IdentityResult result)
        {
            if (result.Succeeded) return;
            var errors = result.Errors.Select(x => x.Description);
            throw new Exception(string.Join('\n', errors));
        }

        private static string ToUrlSafeBase64(string base64String)
        {
            return base64String.Replace('+', '-').Replace('/', '~').Replace('=', '_');
        }

        private static string FromUrlSafeBase64(string urlSafeBase64String)
        {
            return urlSafeBase64String.Replace('-', '+').Replace('~', '/').Replace('_', '=');
        }
    }
}
