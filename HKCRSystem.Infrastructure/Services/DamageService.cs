using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HKCRSystem.Infrastructure.Services
{
    public class DamageService : IDamage
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly IGmailEmailProvider _gmail;
        private readonly UserManager<ApplicationUser> _userManager;

        public DamageService(IApplicationDBContext dContext, UserManager<ApplicationUser> userManager, IGmailEmailProvider gmail)
        {
            _dbContext = dContext;
            _userManager = userManager;
            _gmail = gmail;
        }

        public async Task<ResponseDTO> CreateDamage(DamageRequestDTO model, string id)
        {
            //gets the request and billing
            var request = await _dbContext.Requests.FindAsync(model.RequestId);
            
            //validates request
            if (request == null)
                return new ResponseDTO { Status = "Error", Message = "Request does not exist!" };

            //to check if damage form already exists
            var requestStatus = await _dbContext.Damages.FindAsync(model.RequestId);
            if (requestStatus != null)
                return new ResponseDTO { Status = "Error", Message = "Damage form already submitted!" };

            var billing = new Billing()
            {
                TotalPrice = 0,
                PaymentType = "Cash",
                IsPaid = false,
                AdvancePayment = 0.0,
            };

            var billingEntity = await _dbContext.Billings.AddAsync(billing);

            //creates damage instance
            var damageDetail = new Damage
            {
                DamageDate = model.DamageDate,
                Description = model.Description,
                RequestId = model.RequestId,
                BillingId = billingEntity.Entity.Id,
                CreatedBy = Guid.Parse(id),
                CreatedTime = DateTime.Now.ToUniversalTime(),
            };

            //saves damage data
            await _dbContext.Damages.AddAsync(damageDetail);
            await _dbContext.SaveChangesAsync(default(CancellationToken));

            return new ResponseDTO { Status = "Success", Message = "Damage created successfully." };
        }

        public async Task<ResponseDTO> UpdateDamage(Guid id, string userId, StaffDamageUpdateDTO model)
        {
            //gets the damage
            var damage = await _dbContext.Damages.FindAsync(id);

            if (damage == null)
                return new ResponseDTO { Status = "Error", Message = "Damage update failed!" };
            
            //gets the billing
            var billing = await _dbContext.Billings.FindAsync(damage.BillingId);

            if (billing == null)
                return new ResponseDTO { Status = "Error", Message = "Damage update failed!" };
            
            billing.TotalPrice = model.Price;
            damage.UpdatedBy = Guid.Parse(userId);
            damage.UpdatedTime = DateTime.Now.ToUniversalTime();

            await _dbContext.SaveChangesAsync(default(CancellationToken));

            var user = await _userManager.FindByIdAsync(damage.CreatedBy.ToString());
            //sets the message format
            var message = new EmailMessage
            {
                Subject = "Damage Request Payment",
                To = user.Email,
                Body = @$"Dear {user.FirstName},
                 We have analyzed the car damage submitted by you on {damage.CreatedTime}. We have made a conclusion to charge you with a total of Rs. {billing.TotalPrice} for the damage caused."
            };
            //sends email
            await _gmail.SendEmailAsync(message);
            
            return new ResponseDTO { Status = "Success", Message = "Damage price updated successfully." };
        }

        public async Task<List<DamageResponseDTO>> GetAllDamage()
        {
            //gets the list of damage
            var damages = await _dbContext.Damages
                .Include(d => d.Request.RequestedCar)
                .Include(d => d.Billing)
                .ToListAsync();
            var damageViewModel = new List<DamageResponseDTO>();

            foreach (Damage damage in damages)
            {
                var thisViewModel = new DamageResponseDTO();
                var requestedBy = await _userManager.FindByIdAsync(damage.CreatedBy.ToString());
                var reviewedBy = await _userManager.FindByIdAsync(damage.UpdatedBy.ToString());

                thisViewModel.Id = damage.Id;
                thisViewModel.Description = damage.Description;
                thisViewModel.DamageDate = damage.DamageDate;
                thisViewModel.RequestedBy = $"{requestedBy.FirstName} {requestedBy.LastName}";
                thisViewModel.ReportedDate = damage.CreatedTime;
                thisViewModel.DamagedCar = $"{damage.Request.RequestedCar.Company} {damage.Request.RequestedCar.Model}";
                thisViewModel.ReviewedBy = $"{reviewedBy.FirstName} {reviewedBy.LastName}";
                thisViewModel.Price = damage.Billing.TotalPrice;
                damageViewModel.Add(thisViewModel);
            }

            //returns list
            return damageViewModel;
        }

        public async Task<List<DamageResponseDTO>> GetAllDamageByUserId(string UserId)
        {
            //gets the list of damage
            var damages = await _dbContext.Damages
                .Include(d => d.Request.RequestedCar)
                .Include(d => d.Billing)
                .ToListAsync();

            var damageViewModel = new List<DamageResponseDTO>();

            foreach (Damage damage in damages)
            {
                //filters data of logged in user
                if (damage.CreatedBy.ToString() == UserId)
                {
                    var thisViewModel = new DamageResponseDTO();
                    var requestedBy = await _userManager.FindByIdAsync(damage.CreatedBy.ToString());
                    var reviewedBy = await _userManager.FindByIdAsync(damage.UpdatedBy.ToString());

                    thisViewModel.Id = damage.Id;
                    thisViewModel.Description = damage.Description;
                    thisViewModel.DamageDate = damage.DamageDate;
                    thisViewModel.RequestedBy = $"{requestedBy.FirstName} {requestedBy.LastName}";
                    thisViewModel.ReportedDate = damage.CreatedTime;
                    thisViewModel.DamagedCar = $"{damage.Request.RequestedCar.Company} {damage.Request.RequestedCar.Model}";
                    thisViewModel.ReviewedBy = $"{reviewedBy.FirstName} {reviewedBy.LastName}";
                    thisViewModel.Price = damage.Billing.TotalPrice;

                    damageViewModel.Add(thisViewModel);
                }
            }

            //returns list
            return damageViewModel;
        }
    }
}
