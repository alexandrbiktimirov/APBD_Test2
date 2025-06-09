using Microsoft.AspNetCore.Mvc;
using Test2.DTOs;
using Test2.Exceptions;
using Test2.Services;

namespace Test2.Controllers;

[ApiController]
[Route("api/track-races")]
public class TrackRacesController : ControllerBase
{
    private IDbService _dbService;

    public TrackRacesController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost("participants")]
    public async Task<IActionResult> AddRacerParticipations([FromBody] RacerParticipationsDto dto)
    {
        try
        {
            await _dbService.AddRacerParticipations(dto);
            
            return Created("api/track-races/participants", dto);
        }
        catch (RaceDoesNotExistException e)
        {
            return NotFound(e.Message);
        }
        catch (RacerDoesNotExistException e)
        {
            return NotFound(e.Message);
        }
        catch (TrackDoesNotExistException e)
        {
            return NotFound(e.Message);
        }
        catch (TrackRaceDoesNotExistException e)
        {
            return NotFound(e.Message);
        }
    }
}