﻿using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Infrastructure.Services
{
    public class CarManagementService : ICarManagement
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public CarManagementService(UserManager<ApplicationUser> userManager, IApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ResponseDTO> CreateCar(CarRequestDTO model, string id)
        {
            //creates car instance
            var carDetail = new Car
            {
                Company = model.Company,
                Model = model.Model,
                Price = model.Price,
                Status = model.Status,
                IsAvailable = true,
                CreatedBy = Guid.Parse(id),
                CreatedTime = DateTime.Now.ToUniversalTime(),
            };

            //saves car data
            await _dbContext.Cars.AddAsync(carDetail);
            await _dbContext.SaveChangesAsync(default(CancellationToken));

            return new ResponseDTO { Status = "Success", Message = "Car created successfully." };
        }

        public async Task<List<CarResponseDTO>> GetAllCar()
        {
            //gets the list of car
            var cars = await _dbContext.Cars.ToListAsync();
            var requests = await _dbContext.Requests.ToListAsync();
            var users = await _userManager.Users.ToListAsync();
            //converts into list of response DTO where non-deleted cars is filtered
            var carModel = new List<CarResponseDTO>(
                cars.Where(o => !o.IsDeleted)
                    .Select(o =>
                    {
                        var carRequests = requests.Where(cr => cr.IsApproved && cr.RequestedCarId == o.Id);
                        var enumerable = carRequests as Request[] ?? carRequests.ToArray();
                        var activeCarRequests =
                            enumerable.Where(cr => cr.StartDate >= DateTime.Now && cr.EndDate <= DateTime.Now);
                        return new CarResponseDTO
                        {
                            Id = o.Id,
                            Company = o.Company,
                            Model = o.Model,
                            Price = o.Price,
                            Status = o.Status,
                            IsAvailable = !activeCarRequests.Any(),
                            CreatedBy = users.FirstOrDefault(u => u.Id == o.CreatedBy.ToString())?.FirstName,
                            TimesRented = enumerable.Length
                        };
                    }).ToList()
            );

            //returns list
            return carModel;
        }

        public async Task<ResponseDTO> UpdateCar(CarResponseDTO model, string id)
        {
            //gets car by its id
            var car = await _dbContext.Cars.FindAsync(model.Id);
            if (car == null)
                return new ResponseDTO { Status = "Error", Message = "Car does not exist!" };

            //set new value of car
            car.Company = model.Company;
            car.Model = model.Model;
            car.Price = model.Price;
            car.Status = model.Status;
            car.IsAvailable = model.IsAvailable;
            car.UpdatedBy = Guid.Parse(id);
            car.UpdatedTime = DateTime.Now.ToUniversalTime();

            //updates car
            await _dbContext.SaveChangesAsync(default(CancellationToken));

            return new ResponseDTO { Status = "Success", Message = "Car updated successfully!" };
        }

        public async Task<ResponseDTO> DeleteCar(Guid id, string userId)
        {
            //gets car by its id
            var car = await _dbContext.Cars.FindAsync(id);
            if (car == null)
                return new ResponseDTO { Status = "Error", Message = "Cffer does not exist!" };

            //set new value of offer
            car.DeletedBy = Guid.Parse(userId);
            car.DeletedTime = DateTime.Now.ToUniversalTime();
            car.IsDeleted = true;

            //updates car
            await _dbContext.SaveChangesAsync(default(CancellationToken));

            return new ResponseDTO { Status = "Success", Message = "Car deleted successfully!" };
        }
    }
}