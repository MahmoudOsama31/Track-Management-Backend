using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ITrackDistributionRepository : IGenericRepository<TrackDistribution>
{
    Task<bool> ExistsAsync(int trackId, int dspId);
}
