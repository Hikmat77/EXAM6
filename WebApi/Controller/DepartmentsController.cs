using Application.DTOs.departments;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController(
    IDepartmentService departmentService,
    ILogger<DepartmentsController> logger) : ControllerBase
{
    private IDepartmentService service = departmentService;
    private ILogger<DepartmentsController> _logger = logger;

    [HttpPost]
    public async Task<ApiResponse<bool>> AddDepartment(CreateDepartmentDto dto)
    {
        _logger.LogInformation("POST api/Departments {date}", DateTime.Now);
        return await service.AddDepartmentAsync(dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ApiResponse<bool>> UpdateDepartment(int id, UpdateDepartmentDto dto)
    {
        _logger.LogInformation("PUT api/Departments/{id} {date}", id, DateTime.Now);
        return await service.UpdateDepartmentAsync(id, dto);
    }

    [HttpDelete]
    public async Task<ApiResponse<bool>> DeleteDepartment(int id)
    {
        _logger.LogInformation("DELETE api/Departments?id={id} {date}", id, DateTime.Now);
        return await service.DeleteDepartmentAsync(id);
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<DepartmentDto>> GetDepartment(int id)
    {
        _logger.LogInformation("GET api/Departments/{id} {date}", id, DateTime.Now);
        return await service.GetDepartmentAsync(id);
    }

    [HttpGet]
    public async Task<ApiResponse<List<DepartmentDto>>> GetDepartments()
    {
        _logger.LogInformation("GET api/Departments {date}", DateTime.Now);
        return await service.GetDepartmentsAsync();
    }
}