using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Models;

[Table("Racer")]
public class Racer
{
    public int RacerId { get; set; }
    
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    public ICollection<RaceParticipation> RaceParticipations { get; set; } = new List<RaceParticipation>();
}