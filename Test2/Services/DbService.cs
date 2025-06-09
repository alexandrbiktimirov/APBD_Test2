using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.DTOs;
using Test2.Exceptions;
using Test2.Models;

namespace Test2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<RacerRacesDto> GetRacerRaces(int id)
    {
        var racer = await _context.Racers.FirstOrDefaultAsync(r => r.RacerId == id);

        if (racer == null)
            throw new RacerDoesNotExistException($"Racer with id {id} does not exist");
        
        var participations = await _context.RaceParticipations
            .Where(rp => rp.RacerId == id)
            .Include(rp => rp.TrackRace)
            .ThenInclude(rp => rp.Race)
            .Include(rp => rp.TrackRace)
            .ThenInclude(rp => rp.Track)
            .ToListAsync();

        var participationsDtos = participations.Select(rp => new ParticipationsDto
        {
            Race = new RaceDto
            {
                Name = rp.TrackRace.Race.Name,
                Location = rp.TrackRace.Race.Location,
                Date = rp.TrackRace.Race.Date,
            },
            Track = new TrackDto
            {
                Name = rp.TrackRace.Track.Name,
                LengthInKm = rp.TrackRace.Track.LengthInKm
            },
            Laps = rp.TrackRace.Laps,
            FinishTimeInSeconds = rp.FinishTimeInSeconds,
            Position = rp.Position,
        }).ToList();

        return new RacerRacesDto
        {
            RacerId = racer.RacerId,
            FirstName = racer.FirstName,
            LastName = racer.LastName,
            Participations = participationsDtos,
        };
    }

    public async Task AddRacerParticipations(RacerParticipationsDto dto)
    {
        var race = await _context.Races.FirstOrDefaultAsync(r => r.Name == dto.RaceName);
        
        if (race == null)
            throw new RaceDoesNotExistException($"Race '{dto.RaceName}' does not exist");

        var track = await _context.Tracks.FirstOrDefaultAsync(t => t.Name == dto.TrackName);
        
        if (track == null)
            throw new TrackDoesNotExistException($"Track '{dto.TrackName}' does not exist");

        var trackRace = await _context.TrackRaces.FirstOrDefaultAsync(tr => tr.RaceId == race.RaceId && tr.TrackId == track.TrackId);
        
        if (trackRace == null)
            throw new TrackRaceDoesNotExistException($"No race '{dto.RaceName}' on track '{dto.TrackName}'");

        await using var tx = await _context.Database.BeginTransactionAsync();
        try
        {
            foreach (var participation in dto.Participations)
            {
                if (!await _context.Racers.AnyAsync(r => r.RacerId == participation.RacerId))
                    throw new RacerDoesNotExistException($"Racer with id {participation.RacerId} does not exist");

                var existing = await _context.RaceParticipations.FirstOrDefaultAsync(rp => rp.RacerId == participation.RacerId && rp.TrackRaceId == trackRace.TrackRaceId);

                if (existing != null)
                {
                    if (participation.FinishTimeInSeconds < existing.FinishTimeInSeconds)
                    {
                        existing.FinishTimeInSeconds = participation.FinishTimeInSeconds;
                        existing.Position = participation.Position;
                        _context.RaceParticipations.Update(existing);
                    }
                }
                else
                {
                    var raceParticipation = new RaceParticipation
                    {
                        RacerId = participation.RacerId,
                        TrackRaceId = trackRace.TrackRaceId,
                        FinishTimeInSeconds = participation.FinishTimeInSeconds,
                        Position = participation.Position
                    };
                    
                    await _context.RaceParticipations.AddAsync(raceParticipation);
                }
            }

            await _context.SaveChangesAsync();
            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }
}