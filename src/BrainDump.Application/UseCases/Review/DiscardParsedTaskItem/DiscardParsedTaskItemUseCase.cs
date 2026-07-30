using BrainDump.Domain.Repositories;

namespace BrainDump.Application.UseCases.Review.DiscardParsedTaskItem;

/// <summary>
/// Caso de uso responsável pelo descarte de um rascunho de tarefa.
/// </summary>
public class DiscardParsedTaskItemUseCase
{
    private readonly IParsedTaskItemRepository _parsedTaskItemRepository;

    public DiscardParsedTaskItemUseCase(IParsedTaskItemRepository parsedTaskItemRepository)
    {
        _parsedTaskItemRepository = parsedTaskItemRepository;
    }

    public async Task ExecuteAsync(Guid itemId, Guid userId, CancellationToken cancellationToken = default)
    {
        var item = await _parsedTaskItemRepository.GetByIdAsync(itemId, cancellationToken);
        if (item == null || item.UserId != userId)
        {
            throw new KeyNotFoundException($"Rascunho de tarefa com ID {itemId} não foi encontrado.");
        }

        item.Discard();
        await _parsedTaskItemRepository.UpdateAsync(item, cancellationToken);
    }
}
