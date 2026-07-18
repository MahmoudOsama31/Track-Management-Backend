using Domain.Entities;
using Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<DSP> DSPs => Set<DSP>();
    public DbSet<TrackDistribution> TrackDistributions => Set<TrackDistribution>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Artist>().HasData(SeedData.Artists);
        modelBuilder.Entity<DSP>().HasData(SeedData.DSPs);
        modelBuilder.Entity<Track>().HasData(SeedData.Tracks);
        modelBuilder.Entity<TrackDistribution>().HasData(SeedData.TrackDistributions);
    }
}
