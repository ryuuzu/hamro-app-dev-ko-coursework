using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HKCRSystem.Infrastructure.Services
{
    public class DashboardService : IDashboard
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationDBContext _dbContext;
        public DashboardService(UserManager<ApplicationUser> userManager, IApplicationDBContext dbContext) 
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<ResponseDTO> GetViews()
        {
            //gets total customer
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            var count = customers.Count();

            //gets curent date
            var date = DateTime.UtcNow;
            //gets requests list
            var rent = await _dbContext.Requests.ToListAsync();
            int rentedCount = 0;
            //iterates throgh requests
            foreach(var a in rent) 
            {
                //checks if date lies between start and end date and status is approved
                if (a.StartDate <= date && date <= a.EndDate && a.IsApproved)
                {
                    //icreases count by 1
                    rentedCount += 1;
                }
            }

            //gets the requests that are not approved and is not cancelled
            var pending = _dbContext.Requests.Where(r => !r.IsApproved && !r.IsCancelled && r.RequestedById == r.ApprovedById).Count();

            //gets the number of damage data
            var damage = _dbContext.Damages.Count();

            var Data = new
            {
                customerCount = count,
                rentCount = rentedCount,
                pendingCount = pending,
                damageCount = damage
            };
            string dataJson = JsonSerializer.Serialize(Data);
            return new ResponseDTO { Status = "Success", Message = dataJson };
        }
    }
}
