using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using HKCRSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace HKCRSystem.Infrastructure.Services
{
    public class OfferService : IOffer
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly IGmailEmailProvider _gmail;
        private readonly UserManager<ApplicationUser> _userManager;

        public OfferService(IApplicationDBContext dContext, UserManager<ApplicationUser> userManager, IGmailEmailProvider gmail)
        {
            _dbContext = dContext;
            _userManager = userManager;
            _gmail = gmail;
        }

        public async Task<ResponseDTO> CreateOffer(OfferRequestDTO model, string id)
        {
            //creates Offer instance
            var offerDetail = new Offer
            {
                Name = model.Name,
                Message = model.Message,
                StartDate = model.StartDate.ToUniversalTime(),
                EndDate = model.EndDate.ToUniversalTime(),
                Type = model.Type,
                DiscountPercent = model.DiscountPercent,
                CreatedBy = Guid.Parse(id),
                CreatedTime = DateTime.Now.ToUniversalTime(),
            };

            //saves offer data
            await _dbContext.Offers.AddAsync(offerDetail);
            await _dbContext.SaveChangesAsync(default(CancellationToken));

            //gets the instance of customer
            var users = await _userManager.GetUsersInRoleAsync("Customer");
            //gets the email of customer and sets in list
            var userEmails = users.Select(u => u.Email).ToList();
            
            //sets the message format
            var message = new EmailMessage
            {
                Subject = "Offer! Offer!! Offer!!!",
                To = string.Join(",", userEmails),
                Body = @$"Dear Valuable User,
                 We have a new offer for you. {model.Name} offer start from {model.StartDate} and ends on {model.EndDate}. During this offer you can get {model.DiscountPercent}% discount.
                 Enjoy."
            };
            //sends email
            await _gmail.SendEmailAsync(message);

            return new ResponseDTO { Status = "Success", Message = "Offer created successfully." };
        }

        public async Task<List<OfferResponseDTO>> GetAllOffer()
        {
            //gets the list of offer
            var offers = await _dbContext.Offers.ToListAsync();
            var offerModel = new List<OfferResponseDTO>();

            foreach (Offer offer in offers)
            {
                var thisOfferModel = new OfferResponseDTO();
                var user = await _userManager.FindByIdAsync(offer.CreatedBy.ToString());
                if (!offer.IsDeleted)
                {
                    thisOfferModel.Id = offer.Id;
                    thisOfferModel.Name = offer.Name;
                    thisOfferModel.Message = offer.Message;
                    thisOfferModel.StartDate = offer.StartDate;
                    thisOfferModel.EndDate = offer.EndDate;
                    thisOfferModel.Type = offer.Type;
                    thisOfferModel.DiscountPercent = offer.DiscountPercent;
                    thisOfferModel.CreatedBy = $"{user.FirstName} {user.LastName}";

                    offerModel.Add(thisOfferModel);
                }
            }
            //returns list
            return offerModel;
        }

        public async Task<ResponseDTO> UpdateOffer(OfferResponseDTO model, string id)
        {
            //gets offer by its id
            var offer = await _dbContext.Offers.FindAsync(model.Id);
            if (offer == null)
                return new ResponseDTO { Status = "Error", Message = "Offer does not exist!" };

            //set new value of offer
            offer.Name = model.Name;
            offer.Message = model.Message;
            offer.StartDate = model.StartDate;
            offer.EndDate = model.EndDate;
            offer.DiscountPercent = model.DiscountPercent;
            offer.Type = model.Type;
            offer.UpdatedBy = Guid.Parse(id);
            offer.UpdatedTime = DateTime.Now.ToUniversalTime();

            //updates offer
            await _dbContext.SaveChangesAsync(default(CancellationToken));

            return new ResponseDTO { Status = "Success", Message = "Offer updated successfully!" };
        }

        public async Task<ResponseDTO> DeleteOffer(Guid id, string userId)
        {
            //gets offer by its id
            var offer = await _dbContext.Offers.FindAsync(id);
            if (offer == null)
                return new ResponseDTO { Status = "Error", Message = "Offer does not exist!" };

            //set new value of offer
            offer.DeletedBy = Guid.Parse(userId);
            offer.DeletedTime = DateTime.Now.ToUniversalTime();
            offer.IsDeleted = true;

            //updates offer
            await _dbContext.SaveChangesAsync(default(CancellationToken));

            return new ResponseDTO { Status = "Success", Message = "Offer deleted successfully!" };
        }
    }
}