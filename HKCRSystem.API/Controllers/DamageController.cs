using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HKCRSystem.API.Controllers
{
    [ApiController]
    public class DamageController : ControllerBase
    {
        private readonly IDamage _damage;
        private readonly IHelper _helper;
        private readonly UserManager<ApplicationUser> _userManager;

        public DamageController(IDamage damage, IHelper helper, UserManager<ApplicationUser> userManager)
        {
            _damage = damage;
            _helper = helper;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [Route("/api/add/damage")]
        public async Task<ResponseDTO> CreateDamage([FromBody] DamageRequestDTO model)
        {
            //gets the id from the request header
            string id = _helper.GetIdFromToken(HttpContext);
            var result = await _damage.CreateDamage(model, id);
            return result;
        }

        [HttpGet]
        [Authorize]
        [Route("/api/get/damage")]
        public async Task<List<DamageResponseDTO>> GetAllDamage()
        {
            //gets the id from the request header
            var userId = _helper.GetIdFromToken(HttpContext);
            var user = await _userManager.FindByIdAsync(userId);
            List<DamageResponseDTO> result;

            if (_userManager.GetRolesAsync(user).Result.Contains("Customer"))
            {
                result = await _damage.GetAllDamageByUserId(userId);
            }
            else
            {
                result = await _damage.GetAllDamage();
            }

            return result;
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Staff")]
        [Route("/api/update/damage/{id}")]
        public async Task<ResponseDTO> UpdateDamage([FromRoute] Guid id, [FromBody] StaffDamageUpdateDTO model)
        {
            //gets the id from the request header
            string userId = _helper.GetIdFromToken(HttpContext);
            var result = await _damage.UpdateDamage(id, userId, model);
            return result;
        }

    }
}
