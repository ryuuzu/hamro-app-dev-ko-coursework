using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using HKCRSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Infrastructure.Services
{
    public class UserManagementService : IUserManagement
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationDBContext _dbContext;
        private readonly IUserAuthentication _auth;
        private readonly IHelper _helper;

        public UserManagementService(UserManager<ApplicationUser> userManager, IHelper helper, IApplicationDBContext dbContext, IUserAuthentication auth)
        {
            _userManager = userManager;
            _helper = helper;
            _dbContext = dbContext;
            _auth = auth;
        }

        public async Task<ResponseDTO> AddStaff(StaffAddRequestDTO model)
        {
            //checks the existence of user
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return new ResponseDTO { Status = "Error", Message = "Staff already exists!" };

            //creates the instance of user
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

            //creates user
            var result = await _userManager.CreateAsync(user, model.Password);

            //saving user role
            await _userManager.AddToRoleAsync(user, model.Role);

            if (!result.Succeeded)
                return
                    new ResponseDTO
                    {
                        Status = "Error", Message = "Staff creation failed! Please check staff details and try again."
                    };

            return new ResponseDTO { Status = "Success", Message = "Staff created successfully!" };
        }

        public async Task<List<StaffResponseDTO>> GetAllStaffAsync()
        {
            //gets the user
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<StaffResponseDTO>();
            //iterates through all user
            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new StaffResponseDTO();
                //gets the role of user
                var role = await GetUserRoles(user);
                //checks if user is customer or not, if no adds user detail
                if (role != "Customer")
                {
                    thisViewModel.Id = user.Id;
                    thisViewModel.FirstName = user.FirstName;
                    thisViewModel.Email = user.Email;
                    thisViewModel.LastName = user.LastName;
                    thisViewModel.Address = user.Address;
                    thisViewModel.PhoneNumber = user.PhoneNumber;
                    thisViewModel.Role = role;
                    userRolesViewModel.Add(thisViewModel);
                }
            }

            return userRolesViewModel;
        }

        private async Task<string> GetUserRoles(ApplicationUser user)
        {
            //gets the user role
            string roles = string.Join(",", await _userManager.GetRolesAsync(user));
            return roles;
        }

        public async Task<ResponseDTO> UpdateStaff(StaffResponseDTO model)
        {
            //gets user by its id
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return new ResponseDTO { Status = "Error", Message = "User does not exist!" };
            }

            //update user details
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.FirstName.ToLower();
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            //updates user
            var result = await _userManager.UpdateAsync(user);

            // Remove user from all current roles and add to the new role
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            //saving user role
            await _userManager.AddToRoleAsync(user, model.Role);

            if (!result.Succeeded)
                return
                    new ResponseDTO
                    { Status = "Error", Message = "Staff update failed! Please check staff details and try again." };

            return new ResponseDTO { Status = "Success", Message = "Staff updated successfully!" };
        }

        public async Task<ResponseDTO> DeleteStaff(string id)
        {
            //gets the user by id
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new ResponseDTO { Status = "Error", Message = "User does not exist!" };

            //deletes the user
            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return
                    new ResponseDTO
                        { Status = "Error", Message = "Staff deletion failed!" };

            return new ResponseDTO { Status = "Success", Message = "Staff deleted successfully!" };
        }
        public async Task<ResponseDTO> UpdateProfile(ProfileRequestDTO model, IFormFile file)
        {
            //gets user by its id
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return new ResponseDTO { Status = "Error", Message = "User does not exist!" };
            }

            if (file != null)
            {
                // Validate file
                var validationResult = _helper.ValidateFile(file);
                if (validationResult.Status == "Error")
                {
                    return validationResult;
                }
            }

            //update user details
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.FirstName.ToLower();
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            //updates user
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return
                    new ResponseDTO
                    { Status = "Error", Message = "Profile update failed! Please check staff details and try again." };

            // Check if attachment exists
            var attachment = await _dbContext.Attachments.FirstOrDefaultAsync(a => a.UserId == user.Id);
            if (attachment != null)
            {
                // Update existing attachment with new image
                attachment.Name = Path.GetFileNameWithoutExtension(file.FileName);
                attachment.Type = model.Type;
                attachment.Description = model.Description;
                attachment.Path = Path.Combine("Assest", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                
                using (var stream = new FileStream(attachment.Path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _dbContext.Attachments.Update(attachment);
            }
            else
            {
                await _auth.PostAttachment(file, model.Description, model.Type, model.Id);
            }

            await _dbContext.SaveChangesAsync(default(CancellationToken));

            return new ResponseDTO { Status = "Success", Message = "Profile updated successfully!" };
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomer()
        {
            //gets the user
            var users = await _userManager.Users.ToListAsync();
            var userViewModel = new List<CustomerResponseDTO>();
            //iterates through all user
            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new CustomerResponseDTO();
                //gets the role of user
                var role = await GetUserRoles(user);
                //checks if user is customer or not, if no adds user detail
                if (role == "Customer")
                {
                    thisViewModel.Id = user.Id;
                    thisViewModel.FirstName = user.FirstName;
                    thisViewModel.Email = user.Email;
                    thisViewModel.LastName = user.LastName;
                    thisViewModel.Address = user.Address;
                    thisViewModel.PhoneNumber = user.PhoneNumber;
                    userViewModel.Add(thisViewModel);
                }
            }
            return userViewModel;
        }
    }
}