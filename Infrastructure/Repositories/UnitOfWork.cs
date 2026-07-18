using Application.Interfaces;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Artists = new ArtistRepository(_context);
        Tracks = new TrackRepository(_context);
        DSPs = new DSPRepository(_context);
        TrackDistributions = new TrackDistributionRepository(_context);
    }

    public IArtistRepository Artists { get; }
    public ITrackRepository Tracks { get; }
    public IDSPRepository DSPs { get; }
    public ITrackDistributionRepository TrackDistributions { get; }

    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
}
