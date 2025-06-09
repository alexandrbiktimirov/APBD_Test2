using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Models;

[Table("Track_Race")]
public class TrackRace
{
    [Key]
    public int TrackRaceId { get; set; }
    [ForeignKey(nameof(Track))]
    public int TrackId { get; set; }
    [ForeignKey(nameof(Race))]
    public int RaceId { get; set; }
    public int Laps { get; set; }
    public int? BestTimeInSeconds { get; set; }
    public Track Track { get; set; } = null!;
    public Race Race { get; set; } = null!;
    public ICollection<RaceParticipation> RaceParticipations { get; set; } = new List<RaceParticipation>();
}