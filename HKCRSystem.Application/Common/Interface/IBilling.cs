using HKCRSystem.Application.DTOs;

namespace HKCRSystem.Application.Common.Interface;

public interface IBilling
{
    // Task<BillingResponseDTO> GetBilling(Guid id);
    Task<List<BillingResponseDTO>> GetAllBilling();
}