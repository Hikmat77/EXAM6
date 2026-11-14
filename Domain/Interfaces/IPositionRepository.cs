using Domain.Models;

namespace Domain.Interfaces;

public interface IPositionRepository
{
    Task<bool> AddPositionsAsync(Position positions);
    Task<Position> GetPositionAsync(int id);
    Task<List<Position>> GetPositionsAsync();
}