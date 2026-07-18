using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories;

public interface ITrackRepository : IGenericRepository<Track>
{
    Task<bool> IsrcExistsAsync(string isrc);
    Task<List<Track>> GetFilteredAsync(int? artistId, string? genre, TrackStatus? status);
    Task<Track?> GetWithDetailsAsync(int id);
}
