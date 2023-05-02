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

    public async Task<ResponseDTO> UpdateBilling(Guid id, string userId)
    {
        //gets the instance of bill
        var billing = await _dbContext.Billings.FindAsync(id);
        //validates the presence of bill
        if (billing == null)
            return new ResponseDTO { Status = "Error", Message = "Billing does not exist!" };

        //updates bill
        billing.IsPaid = true;
        billing.UpdatedBy = Guid.Parse(userId);
        billing.UpdatedTime = DateTime.Now.ToUniversalTime();

        await _dbContext.SaveChangesAsync(default(CancellationToken));
        
        return new ResponseDTO { Status = "Success", Message = "Billing marked as paid." };
    }
}