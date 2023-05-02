using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HKCRSystem.Infrastructure.Services;

public class RequestService : IRequest
{
    private readonly IApplicationDBContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public RequestService(UserManager<ApplicationUser> userManager, IApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<ResponseDTO> AcceptRequest(Guid id, string ApprovedById)
    {
        var request = await _dbContext.Requests.FindAsync(id);
        if (request == null)
        {
            return new ResponseDTO
            {
                Status = "Error",
                Message = "Request not found"
            };
        }

        request.ApprovedById = ApprovedById;
        request.IsApproved = true;

        await _dbContext.SaveChangesAsync(default(CancellationToken));

        return new ResponseDTO
        {
            Status = "Error",
            Message = "Request has been accepted!"
        };
    }

    public async Task<ResponseDTO> DenyRequest(Guid id, string ApprovedById)
    {
        var request = await _dbContext.Requests.FindAsync(id);
        if (request == null)
        {
            return new ResponseDTO
            {
                Status = "Error",
                Message = "Request not found"
            };
        }

        request.ApprovedById = ApprovedById;

        await _dbContext.SaveChangesAsync(default(CancellationToken));

        return new ResponseDTO
        {
            Status = "Error",
            Message = "Request has been accepted!"
        };
    }

    public async Task<List<RequestResponseDTO>> GetAllRequest()
    {
        var requests = await _dbContext.Requests.ToListAsync();
        var result = new List<RequestResponseDTO>(
            requests.Select(
                r => new RequestResponseDTO()
                {
                    Id = r.Id,
                    Price = r.Price,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Discount = r.Discount,
                    IsCancelled = r.IsCancelled,
                    RequestedCarId = r.RequestedCarId,
                    RequestedById = r.RequestedById,
                    ApprovedById = r.ApprovedById,
                    BillingId = r.BillingId,
                }
            )
        );

        return result;
    }

    public async Task<List<RequestResponseDTO>> GetAllRequestByUserId(string UserId)
    {
        var requests = await _dbContext.Requests.ToListAsync();
        var result = new List<RequestResponseDTO>(
            requests.Where(r => r.RequestedById == UserId).Select(
                r => new RequestResponseDTO()
                {
                    Id = r.Id,
                    Price = r.Price,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Discount = r.Discount,
                    IsCancelled = r.IsCancelled,
                    RequestedCarId = r.RequestedCarId,
                    RequestedById = r.RequestedById,
                    ApprovedById = r.ApprovedById,
                    BillingId = r.BillingId
                }
            )
        );

        return result;
    }

    public async Task<ResponseDTO> CreateRequest(RequestRequestDTO model, string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        var userAttachment = await _dbContext.Attachments.FindAsync(UserId);
        var requests = await _dbContext.Requests.ToListAsync();
        var car = await _dbContext.Cars.FindAsync(model.RequestedCarId);

        if (userAttachment == null)
        {
            return new ResponseDTO
            {
                Status = "Error",
                Message = "Please update your profile first."
            };
        }


        if (user == null)
        {
            return new ResponseDTO
            {
                Status = "Error",
                Message = "User not found"
            };
        }

        if (car == null)
        {
            return new ResponseDTO
            {
                Status = "Error",
                Message = "Car not found"
            };
        }

        var totalDays = (model.EndDate - model.StartDate).TotalDays;
        var price = car.Price * totalDays;

        var discountPercentage = 0;

        if (_userManager.GetRolesAsync(user).Result.Contains("Customer"))
        {
            var userRequests = requests.Where(r => r.RequestedById == user.Id && r.IsApproved).ToList();
            if (userRequests.Count > 5)
                discountPercentage = 10;
        }
        else
        {
            discountPercentage = 25;
        }

        float discount = (float)(price * discountPercentage / 100);

        var billing = new Billing()
        {
            TotalPrice = price - discount,
            PaymentType = "Cash",
            IsPaid = false,
            AdvancePayment = 0.0,
        };

        var billingEntity = await _dbContext.Billings.AddAsync(billing);

        var request = new Request()
        {
            Price = price,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Discount = discount,
            RequestedCarId = model.RequestedCarId,
            RequestedById = UserId,
            ApprovedById = UserId,
            BillingId = billingEntity.Entity.Id
        };

        var result = await _dbContext.Requests.AddAsync(request);
        await _dbContext.SaveChangesAsync(default(CancellationToken));

        return new ResponseDTO
        {
            Status = "Success",
            Message = "Request has been created!"
        };
    }

    public async Task<ResponseDTO> CancelRequest(Guid id, string userId)
    {
        var request = await _dbContext.Requests.FindAsync(id);
        if (request == null)
        {
            return new ResponseDTO
            {
                Status = "Error",
                Message = "Request not found"
            };
        }

        if (request.RequestedById != userId)
        {
            return new ResponseDTO
            {
                Status = "Error",
                Message = "You are not authorized to cancel this request"
            };
        }

        request.IsCancelled = true;

        await _dbContext.SaveChangesAsync(default(CancellationToken));

        return new ResponseDTO
        {
            Status = "Error",
            Message = "Request has been accepted!"
        };
    }
}