using BrainDump.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrainDump.Infrastructure.Persistence.Configurations;

public class ParsedTaskItemConfiguration : IEntityTypeConfiguration<ParsedTaskItem>
{
    public void Configure(EntityTypeBuilder<ParsedTaskItem> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.VoiceEntryId)
            .IsRequired();

        builder.Property(p => p.UserId)
            .IsRequired();

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(p => p.Category)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.Priority)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.ConfidenceScore)
            .IsRequired();

        builder.HasIndex(p => p.VoiceEntryId);
        builder.HasIndex(p => p.UserId);
        builder.HasIndex(p => p.Status);
    }
}
