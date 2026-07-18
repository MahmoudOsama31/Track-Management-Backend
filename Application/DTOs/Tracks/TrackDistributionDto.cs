using Domain.Enums;

namespace Application.DTOs.Tracks;

public class TrackDistributionDto
{
    public int Id { get; set; }
    public int DSPId { get; set; }
    public string DSPName { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
    public DistributionStatus Status { get; set; }
}
