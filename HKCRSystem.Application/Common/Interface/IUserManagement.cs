using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.Common.Interface
{
    public interface IUserManagement
    {
        Task<ResponseDTO> AddStaff(StaffAddRequestDTO model);
        Task<List<StaffResponseDTO>> GetAllStaffAsync();
        Task<ResponseDTO> UpdateStaff(StaffResponseDTO model);
        Task<ResponseDTO> DeleteStaff(string id);
        Task<ResponseDTO> UpdateProfile(ProfileRequestDTO model, IFormFile file);
        Task<List<CustomerResponseDTO>> GetAllCustomer();
    }
}
