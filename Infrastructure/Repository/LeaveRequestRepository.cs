using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class LeaveRequestRepository(AppDbContext appDbContext) : ILeaveRequestRepository
{
    private readonly AppDbContext _context = appDbContext;

    public async Task<bool> AddLeaveRequestAsync(LeaveRequest leaveRequest)
    {
        await _context.LeaveRequests.AddAsync(leaveRequest);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsAsync(LeaveStatus? status)
    {
        var query = _context.LeaveRequests
            .AsNoTracking()
            .Include(leaveRequest => leaveRequest.Employee)
            .AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(leaveRequest => leaveRequest.Status == status.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<LeaveRequest?> GetLeaveRequestAsync(int id)
    {
        return await _context.LeaveRequests
            .AsNoTracking()
            .Include(leaveRequest => leaveRequest.Employee)
            .FirstOrDefaultAsync(leaveRequest => leaveRequest.Id == id);
    } 

    public async Task<bool> UpdateStatusAsync(int id, LeaveStatus status)
    {
        var leaveRequest = await _context.LeaveRequests.FirstOrDefaultAsync(x => x.Id == id);
        if (leaveRequest is null)
        {
            return false;
        }

        leaveRequest.Status = status;
        await _context.SaveChangesAsync();
        return true;
    }
}