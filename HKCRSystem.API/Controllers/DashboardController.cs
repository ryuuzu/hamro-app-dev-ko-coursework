using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HKCRSystem.API.Controllers
{
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboard _dashboard;

        public DashboardController(IDashboard dashboard)
        {
            _dashboard = dashboard;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin, Staff")]
        [Route("/api/dashboard")]
        public async Task<ResponseDTO> GetView()
        {
            var result = await _dashboard.GetViews();
            return result;
        }
    }
}
