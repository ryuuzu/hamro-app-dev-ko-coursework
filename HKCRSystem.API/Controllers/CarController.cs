using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HKCRSystem.API.Controllers
{
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarManagement _car;
        private readonly IHelper _helper;

        public CarController(ICarManagement car, IHelper helper)
        {
            _car = car;
            _helper = helper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/add/car")]
        public async Task<ResponseDTO> CreateCar([FromBody] CarRequestDTO model)
        {
            //gets the id from the request header
            string id = _helper.GetIdFromToken(HttpContext);
            var result = await _car.CreateCar(model, id);
            return result;
        }


        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/get/car")]
        public async Task<List<CarResponseDTO>> GetAllOffer()
        {
            var result = await _car.GetAllCar();
            return result;
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/update/car")]
        public async Task<ResponseDTO> UpdateCar(CarResponseDTO model)
        {
            //gets the id from the request header
            string id = _helper.GetIdFromToken(HttpContext);
            var result = await _car.UpdateCar(model, id);
            return result;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/delete/car/{id}")]
        public async Task<ResponseDTO> DeleteCar([FromRoute] Guid id)
        {
            //gets the id from the request header
            string userId = _helper.GetIdFromToken(HttpContext);
            var result = await _car.DeleteCar(id, userId);
            return result;
        }
    }
}
