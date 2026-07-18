using Application.Interfaces.Repositories;

namespace Application.Interfaces;

public interface IUnitOfWork
{
    IArtistRepository Artists { get; }
    ITrackRepository Tracks { get; }
    IDSPRepository DSPs { get; }
    ITrackDistributionRepository TrackDistributions { get; }

    Task<int> SaveChangesAsync();
}
