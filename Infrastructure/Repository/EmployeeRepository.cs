using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class EmployeeRepository(AppDbContext appDbContext) : IEmployeeRepository
{
    private readonly AppDbContext _context = appDbContext;
    public async Task<Employee?> GetEmployeeAsync(int id)
    {
        using var connection = _context.Connection();
        var query = "select e.*, d.*, p.* from employees e left join departments d on e.departmentid = d.id left join positions p on e.positionid = p.id where e.id = @id";
        var employees = await connection.QueryAsync<Employee, Department, Position, Employee>(query,(employee, department, position) =>{employee.Department = department; employee.Position = position; return employee;}, new { id }, splitOn: "id,id");
        return employees.FirstOrDefault();
    }

    public async Task<bool> AddEmployeeAsync(Employee employee)
    {
        using var connection = _context.Connection();
        var query = "insert into employees (firstname, lastname, email, phone, hiredate, salary, isactive, departmentid, positionid) values (@FirstName, @LastName, @Email, @Phone, @HireDate, @Salary, @IsActive, @DepartmentId, @PositionId)";
        var res = await connection.ExecuteAsync(query, employee);
        return res > 0;
    }

    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        using var connection = _context.Connection();
        var query = "update employees set firstname = @FirstName, lastname = @LastName, email = @Email, phone = @Phone, hiredate = @HireDate, salary = @Salary, departmentid = @DepartmentId, positionid = @PositionId where id = @Id";
        var res = await connection.ExecuteAsync(query, employee);
        return res > 0;
    }

    public async Task<bool> TerminateEmployeeAsync(int id)
    {
        using var connection = _context.Connection();
        var query = "update employees set isactive = false where id = @id";
        var res = await connection.ExecuteAsync(query, new { id });
        return res > 0;
    }

    public async Task<List<LeaveRequest>> GetEmployeeLeaveRequestsAsync(int employeeId)
    {
        using var connection = _context.Connection();
        var query = "select * from leaverequests where employeeid = @employeeId";
        var leaveRequests = await connection.QueryAsync<LeaveRequest>(query, new { employeeId });
        return leaveRequests.ToList();
    }
}