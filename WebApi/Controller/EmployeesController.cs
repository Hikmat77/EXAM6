using Application.DTOs.employees;
using Application.DTOs.leaverequests;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(
    IEmployeeService employeeService,
    ILogger<EmployeesController> logger) : ControllerBase
{
    private IEmployeeService service = employeeService;
    private ILogger<EmployeesController> _logger = logger;

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<EmployeeDto>> GetEmployee(int id)
    {
        _logger.LogInformation("GET api/Employees/{id} {date}", id, DateTime.Now);
        return await service.GetEmployeeAsync(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> AddEmployee(CreateEmployeeDto dto)
    {
        _logger.LogInformation("POST api/Employees {date}", DateTime.Now);
        return await service.AddEmployeeAsync(dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ApiResponse<bool>> UpdateEmployee(int id, UpdateEmployeeDto dto)
    {
        _logger.LogInformation("PUT api/Employees/{id} {date}", id, DateTime.Now);
        return await service.UpdateEmployeeAsync(id, dto);
    }

    [HttpPost("{id:int}/terminate")]
    public async Task<ApiResponse<bool>> TerminateEmployee(int id)
    {
        _logger.LogInformation("POST api/Employees/{id}/terminate {date}", id, DateTime.Now);
        return await service.TerminateEmployeeAsync(id);
    }

    [HttpGet("{id:int}/leave-requests")]
    public async Task<ApiResponse<List<LeaveRequestDto>>> GetEmployeeLeaveRequests(int id)
    {
        _logger.LogInformation("GET api/Employees/{id}/leave-requests {date}", id, DateTime.Now);
        return await service.GetEmployeeLeaveRequestsAsync(id);
    }
}