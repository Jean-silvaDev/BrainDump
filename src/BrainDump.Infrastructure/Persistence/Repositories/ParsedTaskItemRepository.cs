using BrainDump.Domain.Entities;
using BrainDump.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BrainDump.Infrastructure.Persistence.Repositories;

public class ParsedTaskItemRepository : IParsedTaskItemRepository
{
    private readonly AppDbContext _dbContext;

    public ParsedTaskItemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ParsedTaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ParsedTaskItems
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ParsedTaskItem>> GetByVoiceEntryIdAsync(Guid voiceEntryId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ParsedTaskItems
            .Where(p => p.VoiceEntryId == voiceEntryId)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ParsedTaskItem>> GetPendingByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ParsedTaskItems
            .Where(p => p.UserId == userId && p.Status == ParsedTaskStatus.PendingReview)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<ParsedTaskItem> items, CancellationToken cancellationToken = default)
    {
        await _dbContext.ParsedTaskItems.AddRangeAsync(items, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ParsedTaskItem item, CancellationToken cancellationToken = default)
    {
        _dbContext.ParsedTaskItems.Update(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
