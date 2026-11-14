using System.Net;
using Application.DTOs.leaverequests;
using Application.Interfaces;
using Application.Responses;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class LeaveRequestService(ILeaveRequestRepository leaveRequestRepository) : ILeaveRequestService
{
    private readonly ILeaveRequestRepository _repository = leaveRequestRepository;

    public async Task<ApiResponse<bool>> AddLeaveRequestAsync(CreateLeaveRequestDto dto)
    {
        var leaveRequest = new LeaveRequest()
        {
            EmployeeId = dto.EmployeeId,
            LeaveType = dto.LeaveType,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Reason = dto.Reason,
            Status = LeaveStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
        var res = await _repository.AddLeaveRequestAsync(leaveRequest);
        return res == true
        ? new ApiResponse<bool>(HttpStatusCode.Created, "Added!", res)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not added.", res);
    }

    public async Task<ApiResponse<List<LeaveRequestDto>>> GetLeaveRequestsAsync(LeaveStatus? status)
    {
        var leaveRequests = await _repository.GetLeaveRequestsAsync(status);
        var res = leaveRequests.Select(x => new LeaveRequestDto()
        {
            Id = x.Id,
            EmployeeId = x.EmployeeId,
            EmployeeName = x.Employee != null ? $"{x.Employee.FirstName} {x.Employee.LastName}" : string.Empty,
            LeaveType = x.LeaveType,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            Status = x.Status,
            Reason = x.Reason,
            CreatedAt = x.CreatedAt
        }).ToList();
        return new ApiResponse<List<LeaveRequestDto>>(HttpStatusCode.OK, "List of leave requests.", res);
    }

    public async Task<ApiResponse<bool>> ApproveLeaveRequestAsync(int id)
    {
        var request = await _repository.GetLeaveRequestAsync(id);
        if (request == null)
        {
            return new ApiResponse<bool>(HttpStatusCode.NotFound, "Leave request not found.", false);
        }
        var res = await _repository.UpdateStatusAsync(id, LeaveStatus.Approved);
        return res == true
        ? new ApiResponse<bool>(HttpStatusCode.NoContent, "Approved!", res)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not approved.", res);
    }

    public async Task<ApiResponse<bool>> RejectLeaveRequestAsync(int id)
    {
        var request = await _repository.GetLeaveRequestAsync(id);
        if (request == null)
        {
            return new ApiResponse<bool>(HttpStatusCode.NotFound, "Leave request not found.", false);
        }
        var res = await _repository.UpdateStatusAsync(id, LeaveStatus.Rejected);
        return res == true
        ? new ApiResponse<bool>(HttpStatusCode.NoContent, "Rejected!", res)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not rejected.", res);
    }
}