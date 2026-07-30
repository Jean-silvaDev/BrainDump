using BrainDump.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrainDump.Infrastructure.Persistence.Configurations;

public class VoiceEntryConfiguration : IEntityTypeConfiguration<VoiceEntry>
{
    public void Configure(EntityTypeBuilder<VoiceEntry> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.UserId)
            .IsRequired();

        builder.Property(v => v.AudioFilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(v => v.AudioFormat)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(v => v.DurationSeconds)
            .IsRequired();

        builder.Property(v => v.ByteSize)
            .IsRequired();

        builder.Property(v => v.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(v => v.TranscribedText)
            .HasMaxLength(4000);

        builder.Property(v => v.FailureReason)
            .HasMaxLength(1000);

        builder.HasIndex(v => v.UserId);
        builder.HasIndex(v => v.Status);
    }
}
