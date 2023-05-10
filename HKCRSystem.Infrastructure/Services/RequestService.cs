using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HKCRSystem.Infrastructure.Services;

public class RequestService : IRequest
{
    private readonly IApplicationDBContext _dbContext;
    private readonly IEmailService _emailService;
    private readonly UserManager<ApplicationUser> _userManager;

    public RequestService(UserManager<ApplicationUser> userManager, IEmailService emailService,
        IApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<ResponseDTO> AcceptRequest(Guid id, string ApprovedById)
    {
        var request = (await _dbContext.Requests
            .Include(r => r.RequestedBy)
            .Include(r => r.RequestedCar)
            .ToListAsync()).FirstOrDefault(r => r.Id == id);
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

        await _emailService.SendRequestAcceptedEmailAsync(
            $"{request.RequestedBy.FirstName} {request.RequestedBy.LastName}",
            request.RequestedBy.Email,
            $"{request.RequestedCar.Company} {request.RequestedCar.Model}"
        );

        return new ResponseDTO
        {
            Status = "Success",
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
            Status = "Success",
            Message = "Request has been denied!"
        };
    }

    public async Task<List<RequestResponseDTO>> GetAllRequest()
    {
        var requests = await _dbContext.Requests
            .Include(r => r.RequestedCar)
            .Include(r => r.RequestedBy)
            .Include(r => r.ApprovedBy)
            .ToListAsync();
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
                    RequestedCar = $"{r.RequestedCar.Company} {r.RequestedCar.Model}",
                    RequestedById = r.RequestedById,
                    RequestedBy = $"{r.RequestedBy.FirstName} {r.RequestedBy.LastName}",
                    ApprovedById = r.ApprovedById,
                    ApprovedBy = $"{r.ApprovedBy.FirstName} {r.ApprovedBy.LastName}",
                    BillingId = r.BillingId,
                }
            )
        );

        return result;
    }

    public async Task<List<RequestResponseDTO>> GetAllRequestByUserId(string UserId)
    {
        var requests = await _dbContext.Requests
            .Include(r => r.RequestedCar)
            .Include(r => r.RequestedBy)
            .Include(r => r.ApprovedBy)
            .ToListAsync();
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
                    RequestedCar = $"{r.RequestedCar.Company} {r.RequestedCar.Model}",
                    RequestedById = r.RequestedById,
                    RequestedBy = $"{r.RequestedBy.FirstName} {r.RequestedBy.LastName}",
                    ApprovedById = r.ApprovedById,
                    ApprovedBy = $"{r.ApprovedBy.FirstName} {r.ApprovedBy.LastName}",
                    BillingId = r.BillingId
                }
            )
        );

        return result;
    }

    public async Task<ResponseDTO> CreateRequest(RequestRequestDTO model, string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        var userAttachment = await _dbContext.Attachments.FindAsync(Guid.Parse(UserId));
        var requests = await _dbContext.Requests.ToListAsync();
        // var returns = await _dbContext.Returns
        //     .Include(r => r.Request.RequestedBy)
        //     .Include(r => r.Request.Billing)
        //     .ToListAsync();
        var damages = await _dbContext.Damages
            .Include(d => d.Request)
            .Include(d => d.Billing)
            .ToListAsync();
        var car = await _dbContext.Cars.FindAsync(model.RequestedCarId);

        if (userAttachment == null)
        {
            return new ResponseDTO
            {
                Status = "Error",
                Message = "Please update your profile first."
            };
        }

        var activeCarRequests =
            requests.Where(cr =>
                cr.StartDate >= DateTime.Now && cr.EndDate <= DateTime.Now && cr.IsApproved &&
                cr.RequestedCarId == car.Id);

        if (activeCarRequests.Any())
        {
            return new ResponseDTO
            {
                Status = "Error",
                Message = "Car is not available for the selected dates."
            };
        }

        var userUnpaidDamages = damages.Where(d => d.Request.RequestedById == UserId && !d.Billing.IsPaid).ToList();

        if (userUnpaidDamages.Any())
        {
            return new ResponseDTO
            {
                Status = "Error",
                Message = "Please clear your bills before proceeding with another rent."
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
            StartDate = model.StartDate.ToUniversalTime(),
            EndDate = model.EndDate.ToUniversalTime(),
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
            Status = "Success",
            Message = "Request has been cancelled!"
        };
    }
}