using Dapper;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class LeaveRequestRepository(AppDbContext appDbContext) : ILeaveRequestRepository
{
    private readonly AppDbContext _context = appDbContext;
    public async Task<bool> AddLeaveRequestAsync(LeaveRequest leaveRequest)
    {
        using var connection = _context.Connection();
        var query = "insert into leaverequests (employeeid, leavetype, startdate, enddate, status, reason, createdat) values (@EmployeeId, @LeaveType, @StartDate, @EndDate, @Status, @Reason, @CreatedAt)";
        var res = await connection.ExecuteAsync(query, leaveRequest);
        return res > 0;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsAsync(LeaveStatus? status)
    {
        using var connection = _context.Connection();
        var query = "select lr.*, e.* from leaverequests lr left join employees e on lr.employeeid = e.id where (@Status is null or lr.status = @Status)";
        var leaveRequests = await connection.QueryAsync<LeaveRequest, Employee, LeaveRequest>(query,(leaveRequest, employee) =>{leaveRequest.Employee = employee;return leaveRequest;},new { Status = status },splitOn: "id");
        return leaveRequests.ToList();
    }

    public async Task<LeaveRequest?> GetLeaveRequestAsync(int id)
    {
        using var connection = _context.Connection();
        var query = @"select lr.*, e.* from leaverequests lr left join employees e on lr.employeeid = e.id where lr.id = @id";
        var leaveRequests = await connection.QueryAsync<LeaveRequest, Employee, LeaveRequest>(query,(leaveRequest, employee) =>{leaveRequest.Employee = employee;return leaveRequest;},new { id },splitOn: "id");
        return leaveRequests.FirstOrDefault();
    } 

    public async Task<bool> UpdateStatusAsync(int id, LeaveStatus status)
    {
        using var connection = _context.Connection();
        var query = "update leaverequests set status = @Status where id = @Id";
        var res = await connection.ExecuteAsync(query, new { Id = id, Status = status });
        return res > 0;
    }
}