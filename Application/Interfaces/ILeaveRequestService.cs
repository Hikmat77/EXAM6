using Application.DTOs.leaverequests;
using Application.Responses;
using Domain.Enums;

namespace Application.Interfaces;

public interface ILeaveRequestService
{
    Task<ApiResponse<bool>> AddLeaveRequestAsync(CreateLeaveRequestDto dto);
    Task<ApiResponse<List<LeaveRequestDto>>> GetLeaveRequestsAsync(LeaveStatus? status);
    Task<ApiResponse<bool>> ApproveLeaveRequestAsync(int id);
    Task<ApiResponse<bool>> RejectLeaveRequestAsync(int id);
}