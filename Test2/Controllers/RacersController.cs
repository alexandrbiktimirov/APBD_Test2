using Microsoft.AspNetCore.Mvc;
using Test2.Exceptions;
using Test2.Services;

namespace Test2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RacersController : ControllerBase
{
    private readonly IDbService _dbService;

    public RacersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}/participations")]
    public async Task<IActionResult> GetRacerRaces(int id)
    {
        try
        {
            var result = await _dbService.GetRacerRaces(id);
            return Ok(result);
        }
        catch (RacerDoesNotExistException e)
        {
            return NotFound(e.Message);
        }
    }
}