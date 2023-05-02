using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HKCRSystem.API.Controllers;

[ApiController]
public class RequestController : ControllerBase
{
    private readonly IRequest _request;
    private readonly IHelper _helper;
    private readonly UserManager<ApplicationUser> _userManager;

    public RequestController(UserManager<ApplicationUser> userManager, IRequest request, IHelper helper)
    {
        _request = request;
        _helper = helper;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize]
    [Route("/api/get/request")]
    public async Task<List<RequestResponseDTO>> GetAllRequest()
    {
        //gets the id from the request header
        var userId = _helper.GetIdFromToken(HttpContext);
        var user = await _userManager.FindByIdAsync(userId);
        List<RequestResponseDTO> result;
        if (_userManager.GetRolesAsync(user).Result.Contains("Customer"))
        {
            result = await _request.GetAllRequestByUserId(userId);
        }
        else
        {
            result = await _request.GetAllRequest();
        }

        return result;
    }

    [HttpPost]
    [Authorize]
    [Route("/api/add/request")]
    public async Task<ResponseDTO> CreateRequest([FromBody] RequestRequestDTO model)
    {
        //gets the id from the request header
        var userId = _helper.GetIdFromToken(HttpContext);
        var result = await _request.CreateRequest(model, userId);
        return result;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Staff")]
    [Route("/api/accept/request/{requestId}")]
    public async Task<ResponseDTO> AcceptRequest([FromRoute] Guid requestId)
    {
        //gets the id from the request header
        var userId = _helper.GetIdFromToken(HttpContext);
        var result = await _request.AcceptRequest(requestId, userId);
        return result;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Staff")]
    [Route("/api/deny/request/{requestId}")]
    public async Task<ResponseDTO> DenyRequest([FromRoute] Guid requestId)
    {
        //gets the id from the request header
        var userId = _helper.GetIdFromToken(HttpContext);
        var result = await _request.DenyRequest(requestId, userId);
        return result;
    }

    [HttpGet]
    [Authorize(Roles = "Customer")]
    [Route("/api/cancel/request/{requestId}")]
    public async Task<ResponseDTO> CancelRequest([FromRoute] Guid requestId)
    {
        //gets the id from the request header
        var userId = _helper.GetIdFromToken(HttpContext);
        var result = await _request.CancelRequest(requestId, userId);
        return result;
    }
}