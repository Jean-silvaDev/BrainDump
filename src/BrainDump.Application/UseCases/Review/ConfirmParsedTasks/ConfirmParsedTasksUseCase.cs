using BrainDump.Application.DTOs.Review;
using BrainDump.Application.DTOs.Tasks;
using BrainDump.Domain.Entities;
using BrainDump.Domain.Repositories;

namespace BrainDump.Application.UseCases.Review.ConfirmParsedTasks;

/// <summary>
/// Caso de uso responsável pela confirmação dos rascunhos de tarefas e conversão em tarefas oficiais.
/// </summary>
public class ConfirmParsedTasksUseCase
{
    private readonly IParsedTaskItemRepository _parsedTaskItemRepository;
    private readonly ITaskItemRepository _taskItemRepository;

    public ConfirmParsedTasksUseCase(
        IParsedTaskItemRepository parsedTaskItemRepository,
        ITaskItemRepository taskItemRepository)
    {
        _parsedTaskItemRepository = parsedTaskItemRepository;
        _taskItemRepository = taskItemRepository;
    }

    public async Task<IEnumerable<TaskItemResponse>> ExecuteAsync(
        Guid userId,
        ConfirmTasksRequest request,
        CancellationToken cancellationToken = default)
    {
        var pendingItems = await _parsedTaskItemRepository.GetPendingByUserIdAsync(userId, cancellationToken);
        var pendingList = pendingItems.ToList();

        if (!pendingList.Any())
        {
            return Enumerable.Empty<TaskItemResponse>();
        }

        // Se foram especificados IDs específicos, filtra por eles, senão confirma todos
        var itemsToConfirm = request.ParsedItemIds != null && request.ParsedItemIds.Any()
            ? pendingList.Where(p => request.ParsedItemIds.Contains(p.Id)).ToList()
            : pendingList;

        var createdTasks = new List<TaskItem>();

        foreach (var parsedItem in itemsToConfirm)
        {
            // Marca o rascunho como aprovado
            parsedItem.Approve();
            await _parsedTaskItemRepository.UpdateAsync(parsedItem, cancellationToken);

            // Cria a tarefa oficial confirmada
            var officialTask = TaskItem.Create(
                userId,
                parsedItem.Title,
                parsedItem.Category,
                parsedItem.Priority,
                parsedItem.DueDate);

            createdTasks.Add(officialTask);
        }

        if (createdTasks.Any())
        {
            await _taskItemRepository.AddRangeAsync(createdTasks, cancellationToken);
        }

        return createdTasks.Select(task => new TaskItemResponse(
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
