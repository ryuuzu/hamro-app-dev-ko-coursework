using HKCRSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.Common.Interface
{
    public interface IDamage
    {
        Task<ResponseDTO> CreateDamage(DamageRequestDTO model, string id);
        Task<ResponseDTO> UpdateDamage(Guid id, string userId, StaffDamageUpdateDTO model);
        Task<List<DamageResponseDTO>> GetAllDamage();
        Task<List<DamageResponseDTO>> GetAllDamageByUserId(string UserId);
    }
}
