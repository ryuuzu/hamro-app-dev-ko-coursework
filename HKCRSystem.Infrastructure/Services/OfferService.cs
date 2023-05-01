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

namespace HKCRSystem.Infrastructure.Services
{
    public class OfferService : IOffer
    {
        private readonly IApplicationDBContext _dbContext;

        public OfferService(IApplicationDBContext dContext)
        {
            _dbContext = dContext;
        }

        public async Task<ResponseDTO> CreateOffer(OfferRequestDTO model, string id)
        {
            //creates Offer instance
            var offerDetail = new Offer
            {
                Name = model.Name,
                Message = model.Message,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Type = model.Type,
                DiscountPercent = model.DiscountPercent,
                CreatedBy = Guid.Parse(id),
                CreatedTime = DateTime.Now.ToUniversalTime(),
            };

            //saves offer data
            await _dbContext.Offers.AddAsync(offerDetail);
            await _dbContext.SaveChangesAsync(default(CancellationToken));

            return new ResponseDTO { Status = "Success", Message = "Offer created successfully." };
        }

        public async Task<List<OfferResponseDTO>> GetAllOffer()
        {
            //gets the list of offer
            var offers = await _dbContext.Offers.ToListAsync();
            //converts into list of response DTO where non-deleted offers is filtered
            var offerModel = new List<OfferResponseDTO>(
                offers.Where(o => !o.IsDeleted)
                .Select(o => new OfferResponseDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Message = o.Message,
                    StartDate = o.StartDate,
                    EndDate = o.EndDate,
                    Type = o.Type,
                    DiscountPercent = o.DiscountPercent,
                    CreatedBy = o.CreatedBy,
                }).ToList()
            );

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