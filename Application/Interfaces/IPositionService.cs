using Application.DTOs.positions;
using Application.Responses;

namespace Application.Interfaces;

public interface IPositionService
{
    Task<ApiResponse<bool>> AddPositionAsync(CreatePositionDto dto);
    Task<ApiResponse<PositionDto>> GetPositionAsync(int id);
    Task<ApiResponse<List<PositionDto>>> GetPositionsAsync();
}