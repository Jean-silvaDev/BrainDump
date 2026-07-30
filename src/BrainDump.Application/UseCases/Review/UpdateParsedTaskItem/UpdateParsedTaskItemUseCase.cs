using BrainDump.Application.DTOs.Review;
using BrainDump.Domain.Repositories;

namespace BrainDump.Application.UseCases.Review.UpdateParsedTaskItem;

/// <summary>
/// Caso de uso para atualização dos dados de um rascunho de tarefa durante a revisão.
/// </summary>
public class UpdateParsedTaskItemUseCase
{
    private readonly IParsedTaskItemRepository _parsedTaskItemRepository;

    public UpdateParsedTaskItemUseCase(IParsedTaskItemRepository parsedTaskItemRepository)
    {
        _parsedTaskItemRepository = parsedTaskItemRepository;
    }

    public async Task<ParsedTaskItemResponse> ExecuteAsync(
        Guid itemId,
        Guid userId,
        UpdateParsedTaskItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var item = await _parsedTaskItemRepository.GetByIdAsync(itemId, cancellationToken);
        if (item == null || item.UserId != userId)
        {
            throw new KeyNotFoundException($"Rascunho de tarefa com ID {itemId} não foi encontrado.");
        }

        item.UpdateDetails(request.Title, request.Category, request.Priority, request.DueDate);
        await _parsedTaskItemRepository.UpdateAsync(item, cancellationToken);

        return new ParsedTaskItemResponse(
            item.Id,
            item.VoiceEntryId,
            item.UserId,
            item.Title,
            item.Category,
            item.Priority,
            item.DueDate,
            item.ConfidenceScore,
            item.Status,
            item.CreatedAt);
    }
}
