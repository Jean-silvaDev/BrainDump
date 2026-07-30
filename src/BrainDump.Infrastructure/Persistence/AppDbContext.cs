using BrainDump.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrainDump.Infrastructure.Persistence;

/// <summary>
/// Contexto do Entity Framework Core para persistência dos dados da aplicação.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<VoiceEntry> VoiceEntries => Set<VoiceEntry>();
    public DbSet<ParsedTaskItem> ParsedTaskItems => Set<ParsedTaskItem>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

