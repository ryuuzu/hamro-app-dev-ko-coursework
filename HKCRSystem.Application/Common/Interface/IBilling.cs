using HKCRSystem.Application.DTOs;

namespace HKCRSystem.Application.Common.Interface;

public interface IBilling
{
    Task<List<SalesResponseDTO>> GetAllSales();
    Task<List<BillingResponseDTO>> GetAllBilling();
    Task<ResponseDTO> UpdateBilling(Guid id, string userId);
}