using BrainDump.Domain.Entities;
using BrainDump.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BrainDump.Infrastructure.Persistence.Repositories;

public class VoiceEntryRepository : IVoiceEntryRepository
{
    private readonly AppDbContext _dbContext;

    public VoiceEntryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<VoiceEntry?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.VoiceEntries
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<VoiceEntry>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.VoiceEntries
            .Where(v => v.UserId == userId)
            .OrderByDescending(v => v.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(VoiceEntry voiceEntry, CancellationToken cancellationToken = default)
    {
        await _dbContext.VoiceEntries.AddAsync(voiceEntry, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(VoiceEntry voiceEntry, CancellationToken cancellationToken = default)
    {
        _dbContext.VoiceEntries.Update(voiceEntry);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
