using BrainDump.Application.DTOs.Tasks;
using BrainDump.Domain.Repositories;

namespace BrainDump.Application.UseCases.Tasks.ToggleTaskCompletion;

/// <summary>
/// Caso de uso para alternar o status de conclusão de uma tarefa.
/// </summary>
public class ToggleTaskCompletionUseCase
{
    private readonly ITaskItemRepository _taskItemRepository;

    public ToggleTaskCompletionUseCase(ITaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;
    }

    public async Task<TaskItemResponse> ExecuteAsync(Guid taskId, Guid userId, CancellationToken cancellationToken = default)
    {
        var task = await _taskItemRepository.GetByIdAsync(taskId, cancellationToken);
        if (task == null || task.UserId != userId)
        {
            throw new KeyNotFoundException($"Tarefa com ID {taskId} não foi encontrada.");
        }

        if (task.IsCompleted)
        {
            task.Reopen();
        }
        else
        {
            task.MarkAsCompleted();
        }

        await _taskItemRepository.UpdateAsync(task, cancellationToken);

        return new TaskItemResponse(
            task.Id,
            task.UserId,
            task.Title,
            task.Category,
            task.Priority,
            task.DueDate,
            task.IsCompleted,
            task.CompletedAt,
            task.CreatedAt);
    }
}
