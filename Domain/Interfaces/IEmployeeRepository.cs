using Domain.Models;

namespace Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetEmployeeAsync(int id);
    Task<bool> AddEmployeeAsync(Employee employee);
    Task<bool> UpdateEmployeeAsync(Employee employee);
    Task<bool> TerminateEmployeeAsync(int id);
    Task<List<LeaveRequest>> GetEmployeeLeaveRequestsAsync(int employeeId);
}