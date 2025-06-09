using System.ComponentModel.DataAnnotations;

namespace Test2.DTOs;

public class RacerParticipationsDto
{
    [Required]
    [MaxLength(50)]
    public string RaceName { get; set; } = null!;
    [Required]
    [MaxLength(100)]
    public string TrackName { get; set; } = null!;
    [Required]
    public List<ParticipationsCreateDto> Participations { get; set; } = null!;
}

public class ParticipationsCreateDto
{
    public int RacerId { get; set; }
    public int Position { get; set; }
    public int FinishTimeInSeconds { get; set; }
}