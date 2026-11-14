using Domain.Enums;
using Domain.Models;

namespace Domain.Interfaces;

public interface ILeaveRequestRepository
{
    Task<bool> AddLeaveRequestAsync(LeaveRequest leaveRequest);
    Task<List<LeaveRequest>> GetLeaveRequestsAsync(LeaveStatus? status);
    Task<LeaveRequest?> GetLeaveRequestAsync(int id);
    Task<bool> UpdateStatusAsync(int id, LeaveStatus status);
}