using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKCRSystem.API.Controllers;

[ApiController]
public class BillingController : ControllerBase
{
    private readonly IBilling _billing;

    public BillingController(IBilling billing)
    {
        _billing = billing;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("/api/user/get/billing")]
    public async Task<List<BillingResponseDTO>> GetAllBilling()
    {
        var result = await _billing.GetAllBilling();
        return result;
    }
}