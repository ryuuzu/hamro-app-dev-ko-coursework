using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HKCRSystem.Infrastructure.Services;

public class BillingService : IBilling
{
    private readonly IApplicationDBContext _dbContext;

    public BillingService(IApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    // public async Task<BillingResponseDTO> GetBilling(Guid id)
    // {
    //     var billing = await _dbContext.Billings.FindAsync(id);
    // }

    public async Task<List<BillingResponseDTO>> GetAllBilling()
    {
        var billings = await _dbContext.Billings.ToListAsync();
        var billingModel = new List<BillingResponseDTO>(
            billings.Select(b => new BillingResponseDTO
            {
                Id = b.Id,
                TotalPrice = b.TotalPrice,
                PaymentType = b.PaymentType,
                IsPaid = b.IsPaid,
                AdvancePayment = b.AdvancePayment
            }).ToList()
        );

        return billingModel;
    }
}