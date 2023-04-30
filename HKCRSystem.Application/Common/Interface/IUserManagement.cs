using HKCRSystem.Application.DTOs;
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
    }
}
