using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HKCRSystem.API.Controllers
{
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOffer _offer;

        public OfferController(IOffer offer)
        {
            _offer = offer;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/user/add/offer")]
        public async Task<ResponseDTO> CreateOffer([FromBody] OfferRequestDTO model)
        {
            var result = await _offer.CreateOffer(model);
            return result;
        }


        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/user/get/offer")]
        public async Task<List<OfferResponseDTO>> GetAllOffer()
        {
            var result = await _offer.GetAllOffer();
            return result;
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/user/update/offer")]
        public async Task<ResponseDTO> UpdateOffer(OfferResponseDTO model)
        {
            var result = await _offer.UpdateOffer(model);
            return result;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/user/delete/offer/{id}")]
        public async Task<ResponseDTO> DeleteStaff([FromRoute] Guid id)
        {
            var result = await _offer.DeleteOffer(id);
            return result;
        }
    }
}
