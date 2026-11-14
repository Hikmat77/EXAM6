using System.Net;
using Application.DTOs.departments;
using Application.Responses;

namespace Application.Interfaces;

public interface IDepartmentService
{
    Task<ApiResponse<bool>> AddDepartmentAsync(CreateDepartmentDto dto);
    Task<ApiResponse<bool>> UpdateDepartmentAsync(int id, UpdateDepartmentDto dto);
    Task<ApiResponse<bool>> DeleteDepartmentAsync(int id);
    Task<ApiResponse<DepartmentDto>> GetDepartmentAsync(int id);
    Task<ApiResponse<List<DepartmentDto>>> GetDepartmentsAsync();
}