namespace Test2.DTOs;

public class RacerParticipationsDto
{
    public string RaceName { get; set; } = null!;
    public string TrackName { get; set; } = null!;
    public List<ParticipationsCreateDto> Participations { get; set; } = null!;
}

public class ParticipationsCreateDto
{
    public int RacerId { get; set; }
    public int Position { get; set; }
    public int FinishTimeInSeconds { get; set; }
}