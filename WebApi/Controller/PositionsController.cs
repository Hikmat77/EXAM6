using Application.DTOs.positions;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionsController(
    IPositionService positionService,
    ILogger<PositionsController> logger) : ControllerBase
{
    private IPositionService service = positionService;
    private ILogger<PositionsController> _logger = logger;

    [HttpPost]
    public async Task<ApiResponse<bool>> AddPosition(CreatePositionDto dto)
    {
        _logger.LogInformation("POST api/Positions {date}", DateTime.Now);
        return await service.AddPositionAsync(dto);
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<PositionDto>> GetPosition(int id)
    {
        _logger.LogInformation("GET api/Positions/{id} {date}", id, DateTime.Now);
        return await service.GetPositionAsync(id);
    }

    [HttpGet]
    public async Task<ApiResponse<List<PositionDto>>> GetPositions()
    {
        _logger.LogInformation("GET api/Positions {date}", DateTime.Now);
        return await service.GetPositionsAsync();
    }
}