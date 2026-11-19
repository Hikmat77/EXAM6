using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class DepartmentRepository(AppDbContext appDbContext) : IDepartmentRepository
{
    private readonly AppDbContext _context = appDbContext;

    public async Task<bool> AddDepartmentAsync(Department department)
    {
        await _context.Departments.AddAsync(department);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteDepartmentAsync(int id)
    {
        var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
        if (department is null)
        {
            return false;
        }

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Department> GetDepartmentAsync(int id)
    {
        return await _context.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)
        ?? new Department();
    }

    public async Task<List<Department>> GetDepartmentsAsync()
    {
        return await _context.Departments.AsNoTracking().ToListAsync();
    }

    public async Task<bool> UpdateDepartmentAsync(Department department)
    {
        var existingDepartment = await _context.Departments.FirstOrDefaultAsync(x => x.Id == department.Id);
        if (existingDepartment is null)
        {
            return false;
        }

        existingDepartment.Name = department.Name;
        existingDepartment.Description = department.Description;
        await _context.SaveChangesAsync();
        return true;
    }
}