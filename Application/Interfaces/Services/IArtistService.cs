using Application.DTOs.Artists;

namespace Application.Interfaces.Services;

public interface IArtistService
{
    Task<ArtistDto> CreateAsync(CreateArtistDto dto);
    Task<List<ArtistDto>> GetAllAsync();
}
