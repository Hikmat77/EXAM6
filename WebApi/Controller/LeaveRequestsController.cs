using Application.DTOs.leaverequests;
using Application.Interfaces;
using Application.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveRequestsController(
    ILeaveRequestService leaveRequestService,
    ILogger<LeaveRequestsController> logger) : ControllerBase
{
    private ILeaveRequestService service = leaveRequestService;
    private ILogger<LeaveRequestsController> _logger = logger;

    [HttpPost]
    public async Task<ApiResponse<bool>> AddLeaveRequest(CreateLeaveRequestDto dto)
    {
        _logger.LogInformation("POST api/LeaveRequests {date}", DateTime.Now);
        return await service.AddLeaveRequestAsync(dto);
    }

    [HttpGet]
    public async Task<ApiResponse<List<LeaveRequestDto>>> GetLeaveRequests([FromQuery] LeaveStatus? status)
    {
        _logger.LogInformation("GET api/LeaveRequests {date}", DateTime.Now);
        return await service.GetLeaveRequestsAsync(status);
    }

    [HttpPost("{id:int}/approve")]
    public async Task<ApiResponse<bool>> ApproveLeaveRequest(int id)
    {
        _logger.LogInformation("POST api/LeaveRequests/{id}/approve {date}", id, DateTime.Now);
        return await service.ApproveLeaveRequestAsync(id);
    }

    [HttpPost("{id:int}/reject")]
    public async Task<ApiResponse<bool>> RejectLeaveRequest(int id)
    {
        _logger.LogInformation("POST api/LeaveRequests/{id}/reject {date}", id, DateTime.Now);
        return await service.RejectLeaveRequestAsync(id);
    }
}