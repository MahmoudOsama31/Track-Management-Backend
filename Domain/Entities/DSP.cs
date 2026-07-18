namespace Domain.Entities;

public class DSP
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<TrackDistribution> TrackDistributions { get; set; } = new List<TrackDistribution>();
}
