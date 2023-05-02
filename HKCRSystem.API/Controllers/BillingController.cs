using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKCRSystem.API.Controllers;

[ApiController]
public class BillingController : ControllerBase
{
    private readonly IBilling _billing;
    private readonly IHelper _helper;

    public BillingController(IBilling billing, IHelper helper)
    {
        _billing = billing;
        _helper = helper;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Staff")]
    [Route("api/get/sales")]
    public async Task<List<SalesResponseDTO>> GetAllSales()
    {
        var result = await _billing.GetAllSales();
        return result;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Staff")]
    [Route("/api/user/get/billing")]
    public async Task<List<BillingResponseDTO>> GetAllBilling()
    {
        var result = await _billing.GetAllBilling();
        return result;
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Staff")]
    [Route("/api/update/billing/{id}")]
    public async Task<ResponseDTO> UpdateBilling([FromRoute] Guid id)
    {
        //gets the id from the request header
        string userId = _helper.GetIdFromToken(HttpContext);
        var result = await _billing.UpdateBilling(id, userId);
        return result;
    }
}