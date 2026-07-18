namespace Application.DTOs.Tracks;

public class CreateTrackDto
{
    public string Title { get; set; } = string.Empty;
    public int ArtistId { get; set; }
    public string ISRC { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public string Genre { get; set; } = string.Empty;
}
