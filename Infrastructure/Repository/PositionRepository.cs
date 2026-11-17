using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class PositionRepository(AppDbContext appDbContext) : IPositionRepository
{
    private readonly AppDbContext _context = appDbContext;

    public async Task<bool> AddPositionsAsync(Position positions)
    {
        await _context.Positions.AddAsync(positions);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Position> GetPositionAsync(int id)
    {
        return await _context.Positions.AsNoTracking().SingleAsync(position => position.Id == id);
    }

    public async Task<List<Position>> GetPositionsAsync()
    {
        return await _context.Positions.AsNoTracking().ToListAsync();
    }
}