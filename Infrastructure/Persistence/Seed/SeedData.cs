using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Persistence.Seed;

public static class SeedData
{
    public static Artist[] Artists =>
    [
        new() { Id = 1, Name = "Ava Stone", Email = "ava.stone@example.com", Country = "USA" },
        new() { Id = 2, Name = "Liam Brooks", Email = "liam.brooks@example.com", Country = "UK" },
        new() { Id = 3, Name = "Sofia Rossi", Email = "sofia.rossi@example.com", Country = "Italy" }
    ];

    public static DSP[] DSPs =>
    [
        new() { Id = 1, Name = "Spotify" },
        new() { Id = 2, Name = "Apple Music" },
        new() { Id = 3, Name = "Amazon Music" }
    ];

    public static Track[] Tracks =>
    [
        new() { Id = 1, Title = "Midnight Drive", ArtistId = 1, ISRC = "USRC17600001", ReleaseDate = new DateTime(2025, 1, 10), Genre = "Pop", Status = TrackStatus.Distributed },
        new() { Id = 2, Title = "Golden Hour", ArtistId = 1, ISRC = "USRC17600002", ReleaseDate = new DateTime(2025, 2, 14), Genre = "Pop", Status = TrackStatus.Submitted },
        new() { Id = 3, Title = "Neon Skyline", ArtistId = 1, ISRC = "USRC17600003", ReleaseDate = new DateTime(2025, 3, 5), Genre = "Electronic", Status = TrackStatus.Draft },
        new() { Id = 4, Title = "Silver Rain", ArtistId = 2, ISRC = "GBRC17600004", ReleaseDate = new DateTime(2025, 1, 20), Genre = "Rock", Status = TrackStatus.Distributed },
        new() { Id = 5, Title = "Broken Compass", ArtistId = 2, ISRC = "GBRC17600005", ReleaseDate = new DateTime(2025, 4, 11), Genre = "Rock", Status = TrackStatus.Draft },
        new() { Id = 6, Title = "Paper Boats", ArtistId = 2, ISRC = "GBRC17600006", ReleaseDate = new DateTime(2025, 5, 1), Genre = "Indie", Status = TrackStatus.Submitted },
        new() { Id = 7, Title = "Luna Nera", ArtistId = 3, ISRC = "ITRC17600007", ReleaseDate = new DateTime(2025, 2, 28), Genre = "Jazz", Status = TrackStatus.Draft },
        new() { Id = 8, Title = "Vento del Sud", ArtistId = 3, ISRC = "ITRC17600008", ReleaseDate = new DateTime(2025, 6, 15), Genre = "Jazz", Status = TrackStatus.Distributed }
    ];

    public static TrackDistribution[] TrackDistributions =>
    [
        new() { Id = 1, TrackId = 1, DSPId = 1, SubmittedAt = new DateTime(2025, 1, 12), Status = DistributionStatus.Live },
        new() { Id = 2, TrackId = 1, DSPId = 2, SubmittedAt = new DateTime(2025, 1, 12), Status = DistributionStatus.Live },
        new() { Id = 3, TrackId = 2, DSPId = 1, SubmittedAt = new DateTime(2025, 2, 16), Status = DistributionStatus.Pending },
        new() { Id = 4, TrackId = 4, DSPId = 1, SubmittedAt = new DateTime(2025, 1, 22), Status = DistributionStatus.Live },
        new() { Id = 5, TrackId = 4, DSPId = 3, SubmittedAt = new DateTime(2025, 1, 22), Status = DistributionStatus.Live },
        new() { Id = 6, TrackId = 6, DSPId = 2, SubmittedAt = new DateTime(2025, 5, 3), Status = DistributionStatus.Pending },
        new() { Id = 7, TrackId = 8, DSPId = 1, SubmittedAt = new DateTime(2025, 6, 17), Status = DistributionStatus.Live },
        new() { Id = 8, TrackId = 8, DSPId = 2, SubmittedAt = new DateTime(2025, 6, 17), Status = DistributionStatus.Live },
        new() { Id = 9, TrackId = 8, DSPId = 3, SubmittedAt = new DateTime(2025, 6, 17), Status = DistributionStatus.Rejected }
    ];
}
