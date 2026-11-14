using System.Net;
using Application.DTOs.departments;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class DepartmentService(
    IDepartmentRepository departmentRepository,
    IEmployeeRepository employeeRepository,
    ILogger<DepartmentService> logger) : IDepartmentService
{
    private IDepartmentRepository repository = departmentRepository;
    private IEmployeeRepository emprepository = employeeRepository;
    private ILogger<DepartmentService> _logger = logger;

    public async Task<ApiResponse<bool>> AddDepartmentAsync(CreateDepartmentDto dto)
    {
        var department = new Department()
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };
        var res = await repository.AddDepartmentAsync(department);
        return res == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Added!", res)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not added.", res);
    }

    public async Task<ApiResponse<bool>> DeleteDepartmentAsync(int id)
    {
        var res = await repository.DeleteDepartmentAsync(id);
        return res == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Deleted!", res)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not deleted.", res);
    }

    public async Task<ApiResponse<DepartmentDto>> GetDepartmentAsync(int id)
    {
        var department = await repository.GetDepartmentAsync(id);
        var res = new DepartmentDto()
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            CreatedAt = department.CreatedAt
        };
        return new ApiResponse<DepartmentDto>(HttpStatusCode.OK, "Department with given id.", res);
    }

    public async Task<ApiResponse<List<DepartmentDto>>> GetDepartmentsAsync()
    {
        var departments = await repository.GetDepartmentsAsync();
        var res = departments.Select(x => new DepartmentDto()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            CreatedAt = x.CreatedAt
        }).ToList();
        return new ApiResponse<List<DepartmentDto>>(HttpStatusCode.OK, "List of departments.", res);
    }

    public async Task<ApiResponse<bool>> UpdateDepartmentAsync(int id, UpdateDepartmentDto dto)
    {
        var department = new Department()
        {
            Id = id,
            Name = dto.Name,
            Description = dto.Description
        };

        var res = await repository.UpdateDepartmentAsync(department);
        return res == true
            ? new ApiResponse<bool>(HttpStatusCode.OK, "Updated!", res)
            : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not updated.", res);
    }
}