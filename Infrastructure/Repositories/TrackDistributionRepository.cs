using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrackDistributionRepository : GenericRepository<TrackDistribution>, ITrackDistributionRepository
{
    public TrackDistributionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsAsync(int trackId, int dspId) =>
        await DbSet.AnyAsync(td => td.TrackId == trackId && td.DSPId == dspId);
}
