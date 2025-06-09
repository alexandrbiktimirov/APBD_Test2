using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Race> Races { get; set; } = null!;
    public DbSet<RaceParticipation> RaceParticipations { get; set; } = null!;
    public DbSet<Racer> Racers { get; set; } = null!;
    public DbSet<Track> Tracks { get; set; } = null!;
    public DbSet<TrackRace> TrackRaces { get; set; } = null!;
    
    protected DatabaseContext()
    {
        
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Track>()
            .Property(t => t.LengthInKm)
            .HasPrecision(5, 2);
        
        modelBuilder.Entity<Race>().HasData(new List<Race>
        {
            new() {RaceId = 1, Date = new DateTime(2025, 7, 14), Location = "SilverStone, UK", Name = "British Grand Prix"},
            new() {RaceId = 2, Date = new DateTime(2025, 5, 25), Location = "Monte Carlo, Monaco", Name = "Monaco Grand Prix"},
            new() {RaceId = 3, Date = new DateTime(2025, 5, 1), Location = "Warsaw, Poland", Name = "Poland Grand Prix"},
        });
        
        modelBuilder.Entity<RaceParticipation>().HasData(new List<RaceParticipation>
        {
            new() {RacerId = 1, FinishTimeInSeconds = 5460, Position = 1, TrackRaceId = 1},
            new() {RacerId = 2, FinishTimeInSeconds = 6300, Position = 2, TrackRaceId = 2},
            new() {RacerId = 3, FinishTimeInSeconds = 9000, Position = 3, TrackRaceId = 3}
        });
        
        modelBuilder.Entity<Racer>().HasData(new List<Racer>
        {
            new() {RacerId = 1, FirstName = "Lewis", LastName = "Hamilton"},
            new() {RacerId = 2, FirstName = "John", LastName = "Doe"},
            new() {RacerId = 3, FirstName = "Jane", LastName = "Doe"},
        });

        modelBuilder.Entity<Track>().HasData(new List<Track>
        {
            new() {TrackId = 1, Name = "Silverstone Circuit", LengthInKm = new decimal(5.89)},
            new() {TrackId = 2, Name = "Monaco Circuit", LengthInKm = new decimal(3.34)},
            new() {TrackId = 3, Name = "Warsaw Circuit", LengthInKm = new decimal(3.65)},
        });

        modelBuilder.Entity<TrackRace>().HasData(new List<TrackRace>
        {
            new() {TrackRaceId = 1, RaceId = 1, TrackId = 1, Laps = 52, BestTimeInSeconds = 5460},
            new() {TrackRaceId = 2, RaceId = 2, TrackId = 2, Laps = 78, BestTimeInSeconds = 6588},
            new() {TrackRaceId = 3, RaceId = 3, TrackId = 3, Laps = 75, BestTimeInSeconds = 7599},
        });
    }
}