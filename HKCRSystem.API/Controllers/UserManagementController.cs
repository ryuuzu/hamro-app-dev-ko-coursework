using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HKCRSystem.API.Controllers
{
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagement _userManagement;
        private readonly IHelper _helper;

        public UserManagementController(IUserManagement userManagement, IHelper helper)
        {
            _userManagement = userManagement;
            _helper = helper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/api/user/add/staff")]
        public async Task<ResponseDTO> AddStaff([FromBody] StaffAddRequestDTO model)
        {
            //gets the id from the request header
            string id = _helper.GetIdFromToken(HttpContext);
            var result = await _userManagement.AddStaff(model, id);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/user/get/staff")]
        public async Task<List<StaffResponseDTO>> GetAllStaffAsync()
        {
            //gets the id from the request header
            string id = _helper.GetIdFromToken(HttpContext);
            var result = await _userManagement.GetAllStaffAsync(id);
            return result;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("/api/user/update/staff")]
        public async Task<ResponseDTO> UpdateStaff([FromBody] StaffResponseDTO model)
        {
            var result = await _userManagement.UpdateStaff(model);
            return result;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("/api/user/delete/staff/{id}")]
        public async Task<ResponseDTO> DeleteStaff([FromRoute] string id)
        {
            var result = await _userManagement.DeleteStaff(id);
            return result;
        }

        [HttpPut]
        [Authorize]
        [Route("/api/user/update/profile")]
        public async Task<ResponseDTO> UpdateProfile([FromForm] ProfileRequestDTO model, IFormFile file)
        {
            var result = await _userManagement.UpdateProfile(model, file);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/user/get/customer")]
        public async Task<List<CustomerResponseDTO>> GetAllCustomer()
        {
            var result = await _userManagement.GetAllCustomer();
            return result;
        }
    }
}
