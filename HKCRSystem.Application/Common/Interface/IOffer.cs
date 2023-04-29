using HKCRSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.Common.Interface
{
    public interface IOffer
    {
        Task<ResponseDTO> CreateOffer(OfferRequestDTO model);
        Task<List<OfferResponseDTO>> GetAllOffer();
        Task<ResponseDTO> UpdateOffer(OfferResponseDTO model);
        Task<ResponseDTO> DeleteOffer(Guid id);
    }
}
