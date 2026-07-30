using BrainDump.Domain.Repositories;

namespace BrainDump.Application.UseCases.Tasks.DeleteTask;

/// <summary>
/// Caso de uso para exclusão de uma tarefa.
/// </summary>
public class DeleteTaskUseCase
{
    private readonly ITaskItemRepository _taskItemRepository;

    public DeleteTaskUseCase(ITaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;
    }

    public async Task ExecuteAsync(Guid taskId, Guid userId, CancellationToken cancellationToken = default)
    {
        var task = await _taskItemRepository.GetByIdAsync(taskId, cancellationToken);
        if (task == null || task.UserId != userId)
        {
            throw new KeyNotFoundException($"Tarefa com ID {taskId} não foi encontrada.");
        }

        await _taskItemRepository.DeleteAsync(task, cancellationToken);
    }
}
