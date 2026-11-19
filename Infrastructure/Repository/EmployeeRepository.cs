using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class EmployeeRepository(AppDbContext appDbContext) : IEmployeeRepository
{
    private readonly AppDbContext _context = appDbContext;

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        return await _context.Employees
            .AsNoTracking()
            .Include(employee => employee.Department)
            .Include(employee => employee.Position)
            .ToListAsync();
    }

    public async Task<Employee?> GetEmployeeAsync(int id)
    {
        return await _context.Employees
            .AsNoTracking()
            .Include(employee => employee.Department)
            .Include(employee => employee.Position)
            .FirstOrDefaultAsync(employee => employee.Id == id);
    }

    public async Task<bool> AddEmployeeAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        var existingEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
        if (existingEmployee is null)
        {
            return false;
        }

        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.Email = employee.Email;
        existingEmployee.Phone = employee.Phone;
        existingEmployee.HireDate = employee.HireDate;
        existingEmployee.Salary = employee.Salary;
        existingEmployee.DepartmentId = employee.DepartmentId;
        existingEmployee.PositionId = employee.PositionId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TerminateEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        if (employee is null)
        {
            return false;
        }

        employee.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<LeaveRequest>> GetEmployeeLeaveRequestsAsync(int employeeId)
    {
        return await _context.LeaveRequests
            .AsNoTracking()
            .Where(leaveRequest => leaveRequest.EmployeeId == employeeId)
            .ToListAsync();
    }
}