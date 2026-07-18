using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrackRepository : GenericRepository<Track>, ITrackRepository
{
    public TrackRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> IsrcExistsAsync(string isrc) =>
        await DbSet.AnyAsync(t => t.ISRC == isrc);

    public async Task<List<Track>> GetFilteredAsync(int? artistId, string? genre, TrackStatus? status)
    {
        var query = DbSet.Include(t => t.Artist).AsQueryable();

        if (artistId.HasValue)
            query = query.Where(t => t.ArtistId == artistId.Value);

        if (!string.IsNullOrWhiteSpace(genre))
            query = query.Where(t => t.Genre == genre);

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        return await query.ToListAsync();
    }

    public async Task<Track?> GetWithDetailsAsync(int id) =>
        await DbSet
            .Include(t => t.Artist)
            .Include(t => t.TrackDistributions)
                .ThenInclude(td => td.DSP)
            .FirstOrDefaultAsync(t => t.Id == id);
}
