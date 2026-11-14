using System.Net;
using Application.DTOs.positions;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class PositionService(IPositionRepository positionRepository) : IPositionService
{
    private IPositionRepository repository = positionRepository;

    public async Task<ApiResponse<bool>> AddPositionAsync(CreatePositionDto dto)
    {
        var position = new Position()
        {
            Title = dto.Title,
            MinSalary = dto.MinSalary,
            MaxSalary = dto.MaxSalary
        };
        var res = await repository.AddPositionsAsync(position);
        return res == true
        ? new ApiResponse<bool>(HttpStatusCode.OK, "Added!", res)
        : new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Not added.", res);
    }

    public async Task<ApiResponse<PositionDto>> GetPositionAsync(int id)
    {
        var position = await repository.GetPositionAsync(id);
        var res = new PositionDto()
        {
            Id = position.Id,
            Title = position.Title,
            MinSalary = position.MinSalary,
            MaxSalary = position.MaxSalary
        };
        return new ApiResponse<PositionDto>(HttpStatusCode.OK, "Position with given id.", res);
    }

    public async Task<ApiResponse<List<PositionDto>>> GetPositionsAsync()
    {
        var positions = await repository.GetPositionsAsync();
        var res = positions.Select(x => new PositionDto()
        {
            Id = x.Id,
            Title = x.Title,
            MinSalary = x.MinSalary,
            MaxSalary = x.MaxSalary
        }).ToList();
        return new ApiResponse<List<PositionDto>>(HttpStatusCode.OK, "List of positions.", res);
    }
}