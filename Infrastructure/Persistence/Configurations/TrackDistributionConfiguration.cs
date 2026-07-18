using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TrackDistributionConfiguration : IEntityTypeConfiguration<TrackDistribution>
{
    public void Configure(EntityTypeBuilder<TrackDistribution> builder)
    {
        builder.HasKey(td => td.Id);

        builder.Property(td => td.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(td => td.SubmittedAt)
            .IsRequired();

        builder.HasIndex(td => new { td.TrackId, td.DSPId }).IsUnique();

        builder.HasOne(td => td.Track)
            .WithMany(t => t.TrackDistributions)
            .HasForeignKey(td => td.TrackId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(td => td.DSP)
            .WithMany(d => d.TrackDistributions)
            .HasForeignKey(td => td.DSPId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
