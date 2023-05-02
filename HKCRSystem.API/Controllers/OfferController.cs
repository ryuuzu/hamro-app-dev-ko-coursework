using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Infrastructure.Services;
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
        private readonly IHelper _helper;

        public OfferController(IOffer offer, IHelper helper)
        {
            _offer = offer;
            _helper = helper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/add/offer")]
        public async Task<ResponseDTO> CreateOffer([FromBody] OfferRequestDTO model)
        {
            //gets the id from the request header
            string id = _helper.GetIdFromToken(HttpContext);
            var result = await _offer.CreateOffer(model, id);
            return result;
        }


        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/get/offer")]
        public async Task<List<OfferResponseDTO>> GetAllOffer()
        {
            var result = await _offer.GetAllOffer();
            return result;
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/update/offer")]
        public async Task<ResponseDTO> UpdateOffer(OfferResponseDTO model)
        {
            //gets the id from the request header
            string id = _helper.GetIdFromToken(HttpContext);
            var result = await _offer.UpdateOffer(model, id);
            return result;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/delete/offer/{id}")]
        public async Task<ResponseDTO> DeleteOffer([FromRoute] Guid id)
        {
            //gets the id from the request header
            string userId = _helper.GetIdFromToken(HttpContext);
            var result = await _offer.DeleteOffer(id, userId);
            return result;
        }
    }
}