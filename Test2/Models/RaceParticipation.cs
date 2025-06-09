using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test2.Models;

[Table("Race_Participation")]
[PrimaryKey(nameof(TrackRaceId), nameof(RacerId))]
public class RaceParticipation
{
    [ForeignKey(nameof(TrackRace))]
    public int TrackRaceId { get; set; }
    [ForeignKey(nameof(Racer))]
    public int RacerId { get; set; }
    
    public int FinishTimeInSeconds { get; set; }
    
    public int Position { get; set; }
    
    public TrackRace TrackRace { get; set; } = null!;
    public Racer Racer { get; set; } = null!;
}
