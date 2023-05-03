using HKCRSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.Common.Interface
{
    public interface ICarManagement
    {
        Task<ResponseDTO> CreateCar(CarRequestDTO model, string id);
        Task<List<CarResponseDTO>> GetAllCar();
        Task<ResponseDTO> UpdateCar(CarResponseDTO model, string id);
        Task<ResponseDTO> DeleteCar(Guid id, string userId);
    }
}
