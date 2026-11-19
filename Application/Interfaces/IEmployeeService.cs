using Application.DTOs.employees;
using Application.DTOs.leaverequests;
using Application.Responses;

namespace Application.Interfaces;

public interface IEmployeeService
{
    Task<ApiResponse<List<EmployeeDto>>> GetEmployeesAsync();
    Task<ApiResponse<EmployeeDto>> GetEmployeeAsync(int id);
    Task<ApiResponse<bool>> AddEmployeeAsync(CreateEmployeeDto dto);
    Task<ApiResponse<bool>> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto);
    Task<ApiResponse<bool>> TerminateEmployeeAsync(int id);
    Task<ApiResponse<List<LeaveRequestDto>>> GetEmployeeLeaveRequestsAsync(int id);
}