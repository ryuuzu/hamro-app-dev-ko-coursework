using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Infrastructure.Services
{
    public class ReturnService : IReturn
    {
        private readonly IApplicationDBContext _dbContext;

        public ReturnService(IApplicationDBContext dContext)
        {
            _dbContext = dContext;
        }

        public async Task<ResponseDTO> CreateReturn(ReturnRequestDTO model, string id)
        {
            //creates return instance
            var returnDetail = new Return
            {
                RequestId = model.RequestId,
                AcceptedBy = id,
                ReturnDate = DateTime.Now.ToUniversalTime(),
                CreatedBy = Guid.Parse(id),
                CreatedTime = DateTime.Now.ToUniversalTime(),
            };

            //saves return data
            await _dbContext.Returns.AddAsync(returnDetail);
            await _dbContext.SaveChangesAsync(default(CancellationToken));

            return new ResponseDTO { Status = "Success", Message = "Car returned successfully." };
        }

        public async Task<List<ReturnResponseDTO>> GetAllReturn()
        {
            //gets the list of return
            var returns = await _dbContext.Returns.ToListAsync();
            //converts into list of response DTO 
            var returnModel = new List<ReturnResponseDTO>(
                returns.Select(o => new ReturnResponseDTO
                {
                    Id = o.Id,
                    AcceptedBy = $"{o.AcceptBy.FirstName} {o.AcceptBy.LastName}",
                    ReturnDate = o.ReturnDate,
                    RequestId = o.RequestId,
                    UserName = $"{o.Request.RequestedBy.FirstName} {o.Request.RequestedBy.LastName}",
                    CarName = $"{o.Request.RequestedCar.Company} {o.Request.RequestedCar.Model}"
                }).ToList()
            );

            //returns list
            return returnModel;
        }
    }
}
