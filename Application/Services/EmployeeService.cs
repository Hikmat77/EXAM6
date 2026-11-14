using System.Net;
using Application.DTOs.employees;
using Application.DTOs.leaverequests;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _repository = employeeRepository;
    public async Task<ApiResponse<EmployeeDto>> GetEmployeeAsync(int id)
    {
        var employee = await _repository.GetEmployeeAsync(id);
        if (employee == null)
        {
            return new ApiResponse<EmployeeDto>(HttpStatusCode.NotFound, "Employee not found.", null!);
        }

        var res = new EmployeeDto()
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Phone = employee.Phone,
            HireDate = employee.HireDate,
            Salary = employee.Salary,
            IsActive = employee.IsActive,
            DepartmentId = employee.DepartmentId,
            DepartmentName = employee.Department?.Name ?? string.Empty,
            PositionId = employee.PositionId,
            PositionTitle = employee.Position?.Title ?? string.Empty
        };

        return new ApiResponse<EmployeeDto>(HttpStatusCode.OK, "Employee with given id.", res);
    }

    public async Task<ApiResponse<bool>> AddEmployeeAsync(CreateEmployeeDto dto)
    {
        var employee = new Employee()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone ?? string.Empty,
            HireDate = dto.HireDate,
            Salary = dto.Salary,
            DepartmentId = dto.DepartmentId,
            PositionId = dto.PositionId,
            IsActive = true
        };

        var res = await _repository.AddEmployeeAsync(employee);
        return res == true
            ? new ApiResponse<bool>(HttpStatusCode.Created, "Added!", res)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not added.", res);
    }

    public async Task<ApiResponse<bool>> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto)
    {
        var employee = new Employee()
        {
            Id = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone ?? string.Empty,
            HireDate = dto.HireDate,
            Salary = dto.Salary,
            DepartmentId = dto.DepartmentId,
            PositionId = dto.PositionId
        };

        var res = await _repository.UpdateEmployeeAsync(employee);
        return res == true
            ? new ApiResponse<bool>(HttpStatusCode.NoContent, "Updated!", res)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not updated.", res);
    }

    public async Task<ApiResponse<bool>> TerminateEmployeeAsync(int id)
    {
        var res = await _repository.TerminateEmployeeAsync(id);
        return res == true
        ? new ApiResponse<bool>(HttpStatusCode.NoContent, "Terminated!", res)
        : new ApiResponse<bool>(HttpStatusCode.NotFound, "Employee not found.", res);
    }

    public async Task<ApiResponse<List<LeaveRequestDto>>> GetEmployeeLeaveRequestsAsync(int id)
    {
        var leaveRequests = await _repository.GetEmployeeLeaveRequestsAsync(id);
        var res = leaveRequests.Select(x => new LeaveRequestDto()
        {
            Id = x.Id,
            EmployeeId = x.EmployeeId,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            Reason = x.Reason,
            Status = x.Status
        }).ToList();
        return new ApiResponse<List<LeaveRequestDto>>(HttpStatusCode.OK, "List of employee leave requests.", res);
    }
}