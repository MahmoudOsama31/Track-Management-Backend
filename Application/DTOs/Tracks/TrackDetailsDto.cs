using Application.DTOs.Artists;
using Domain.Enums;

namespace Application.DTOs.Tracks;

public class TrackDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ISRC { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public string Genre { get; set; } = string.Empty;
    public TrackStatus Status { get; set; }
    public ArtistDto Artist { get; set; } = null!;
    public List<TrackDistributionDto> Distributions { get; set; } = new();
}
