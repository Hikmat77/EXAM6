using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class DepartmentRepository(AppDbContext appDbContext) : IDepartmentRepository
{
    private AppDbContext _context = appDbContext;
    public async Task<bool> AddDepartmentAsync(Department department)
    {
        using var connection = _context.Connection();
        var query = "insert into departments (name, description, createat) values (@name, @description, @createdat)";
        var res = await connection.ExecuteAsync(query, department);
        return res>0;
    }

    public async Task<bool> DeleteDepartmentAsync(int id)
    {
        using var connection = _context.Connection();
        var query = $"delete from departments where id = @id";
        var res = await connection.ExecuteAsync(query, new{id});
        return res>0;
    }

    public async Task<Department> GetDepartmentAsync(int id)
    {
        using var connection = _context.Connection();
        var query = $"select * from departments where id = @id";
        var department = await connection.QuerySingleAsync<Department>(query, new{id}); 
        return department;
    }

    public async Task<List<Department>> GetDepartmentsAsync()
    {
        using var connection = _context.Connection();
        var query = $"select * from departments";
        var author = await connection.QueryAsync<Department>(query);
        return author.ToList();
    }

    public async Task<bool> UpdateDepartmentAsync(Department department)
    {
        using var connection = _context.Connection();
        var query = $"update departments set name = @name, description = @description, createdat = @createdat where id = @id";
        var res = await connection.ExecuteAsync(query, department);
        return res>0;
    }
}