using BrainDump.Domain.Entities;
using BrainDump.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BrainDump.Infrastructure.Persistence.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly AppDbContext _dbContext;

    public TaskItemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.TaskItems
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.TaskItems
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TaskItem>> GetFilteredAsync(
        Guid userId,
        Category? category = null,
        Priority? priority = null,
        bool? isCompleted = null,
        string? searchTerm = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.TaskItems.AsNoTracking().Where(t => t.UserId == userId);

        if (category.HasValue)
        {
            query = query.Where(t => t.Category == category.Value);
        }

        if (priority.HasValue)
        {
            query = query.Where(t => t.Priority == priority.Value);
        }

        if (isCompleted.HasValue)
        {
            query = query.Where(t => t.IsCompleted == isCompleted.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.Trim().ToLower();
            query = query.Where(t => t.Title.ToLower().Contains(search));
        }

        return await query
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.DueDate ?? DateTime.MaxValue)
            .ThenByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }


    public async Task AddAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
    {
        await _dbContext.TaskItems.AddAsync(taskItem, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<TaskItem> taskItems, CancellationToken cancellationToken = default)
    {
        await _dbContext.TaskItems.AddRangeAsync(taskItems, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
    {
        _dbContext.TaskItems.Update(taskItem);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
    {
        _dbContext.TaskItems.Remove(taskItem);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
