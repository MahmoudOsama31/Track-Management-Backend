using Domain.Enums;

namespace Domain.Entities;

public class TrackDistribution
{
    public int Id { get; set; }
    public int TrackId { get; set; }
    public int DSPId { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DistributionStatus Status { get; set; } = DistributionStatus.Pending;

    public Track Track { get; set; } = null!;
    public DSP DSP { get; set; } = null!;
}
