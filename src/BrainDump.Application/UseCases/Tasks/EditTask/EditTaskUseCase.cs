using BrainDump.Application.DTOs.Tasks;
using BrainDump.Domain.Repositories;

namespace BrainDump.Application.UseCases.Tasks.EditTask;

/// <summary>
/// Caso de uso para edição das propriedades de uma tarefa oficial.
/// </summary>
public class EditTaskUseCase
{
    private readonly ITaskItemRepository _taskItemRepository;

    public EditTaskUseCase(ITaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;
    }

    public async Task<TaskItemResponse> ExecuteAsync(
        Guid taskId,
        Guid userId,
        EditTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var task = await _taskItemRepository.GetByIdAsync(taskId, cancellationToken);
        if (task == null || task.UserId != userId)
        {
            throw new KeyNotFoundException($"Tarefa com ID {taskId} não foi encontrada.");
        }

        task.UpdateDetails(request.Title, request.Category, request.Priority, request.DueDate);
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
