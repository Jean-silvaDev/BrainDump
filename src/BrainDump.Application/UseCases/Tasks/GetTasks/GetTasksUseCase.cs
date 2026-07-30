using BrainDump.Application.DTOs.Tasks;
using BrainDump.Domain.Repositories;

namespace BrainDump.Application.UseCases.Tasks.GetTasks;

/// <summary>
/// Caso de uso responsável pela consulta e filtragem de tarefas do usuário.
/// </summary>
public class GetTasksUseCase
{
    private readonly ITaskItemRepository _taskItemRepository;

    public GetTasksUseCase(ITaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;
    }

    public async Task<IEnumerable<TaskItemResponse>> ExecuteAsync(
        Guid userId,
        GetTasksQuery query,
        CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("ID do usuário inválido.", nameof(userId));

        var tasks = await _taskItemRepository.GetFilteredAsync(
            userId,
            query.Category,
            query.Priority,
            query.IsCompleted,
            query.SearchTerm,
            cancellationToken);

        return tasks.Select(task => new TaskItemResponse(
            task.Id,
            task.UserId,
            task.Title,
            task.Category,
            task.Priority,
            task.DueDate,
            task.IsCompleted,
            task.CompletedAt,
            task.CreatedAt));
    }
}
