using HKCRSystem.Application.DTOs;

namespace HKCRSystem.Application.Common.Interface;

public interface IRequest
{
    Task<ResponseDTO> AcceptRequest(Guid id, string ApprovedById);
    Task<ResponseDTO> DenyRequest(Guid id, string ApprovedById);
    Task<List<RequestResponseDTO>> GetAllRequest();
    Task<List<RequestResponseDTO>> GetAllRequestByUserId(string UserId);
    Task<ResponseDTO> CreateRequest(RequestRequestDTO model, string UserId);
    Task<ResponseDTO> CancelRequest(Guid id, string userId);
}