using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class PositionRepository(AppDbContext appDbContext) : IPositionRepository
{
    private AppDbContext _context = appDbContext;
    public async Task<bool> AddPositionsAsync(Position positions)
    {
        using var connection = _context.Connection();
        var query = "insert into positions (title, minsalary, maxsalary) values (@title, @minsalary, @maxsalary)";
        var res = await connection.ExecuteAsync(query, positions);
        return res>0;
    }

    public async Task<Position> GetPositionAsync(int id)
    {
        using var connection = _context.Connection();
        var query = $"select * from positions where id = @id";
        var position = await connection.QuerySingleAsync<Position>(query, new{id});
        return position;
    }

    public async Task<List<Position>> GetPositionsAsync()
    {
        using var connection = _context.Connection();
        var query = $"select * from positions";
        var position = await connection.QueryAsync<Position>(query);
        return position.ToList();
    }
}