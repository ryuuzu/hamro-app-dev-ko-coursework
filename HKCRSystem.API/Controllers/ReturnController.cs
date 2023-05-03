using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HKCRSystem.API.Controllers
{
    [ApiController]
    public class ReturnController : ControllerBase
    {
        private readonly IReturn _return;
        private readonly IHelper _helper;

        public ReturnController(IReturn returnCar, IHelper helper)
        {
            _return = returnCar;
            _helper = helper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/return/car")]
        public async Task<ResponseDTO> CreateReturn([FromBody] ReturnRequestDTO model)
        {
            //gets the id from the request header
            string id = _helper.GetIdFromToken(HttpContext);
            var result = await _return.CreateReturn(model, id);
            return result;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/get/return-car")]
        public async Task<List<ReturnResponseDTO>> GetAllReturn()
        {
            var result = await _return.GetAllReturn();
            return result;
        }
    }
}
