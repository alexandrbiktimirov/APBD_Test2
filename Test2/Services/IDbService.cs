using Test2.DTOs;

namespace Test2.Services;

public interface IDbService
{
    public Task<RacerRacesDto> GetRacerRaces(int id);
    public Task AddRacerParticipations(RacerParticipationsDto dto);
}