using BrainDump.Domain.Entities;

namespace BrainDump.Domain.Repositories;

/// <summary>
/// Contrato do repositório para persistência de tarefas confirmadas (TaskItem).
/// </summary>
public interface ITaskItemRepository
{
    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskItem>> GetFilteredAsync(
        Guid userId,
        Category? category = null,
        Priority? priority = null,
        bool? isCompleted = null,
        string? searchTerm = null,
        CancellationToken cancellationToken = default);
    Task AddAsync(TaskItem taskItem, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<TaskItem> taskItems, CancellationToken cancellationToken = default);
    Task UpdateAsync(TaskItem taskItem, CancellationToken cancellationToken = default);
    Task DeleteAsync(TaskItem taskItem, CancellationToken cancellationToken = default);
}
