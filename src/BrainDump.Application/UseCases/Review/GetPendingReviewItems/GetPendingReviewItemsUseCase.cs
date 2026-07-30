using BrainDump.Application.DTOs.Review;
using BrainDump.Domain.Repositories;

namespace BrainDump.Application.UseCases.Review.GetPendingReviewItems;

/// <summary>
/// Caso de uso que lista todos os rascunhos de tarefas pendentes de revisão para o usuário.
/// </summary>
public class GetPendingReviewItemsUseCase
{
    private readonly IParsedTaskItemRepository _parsedTaskItemRepository;

    public GetPendingReviewItemsUseCase(IParsedTaskItemRepository parsedTaskItemRepository)
    {
        _parsedTaskItemRepository = parsedTaskItemRepository;
    }

    public async Task<IEnumerable<ParsedTaskItemResponse>> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("ID de usuário inválido.", nameof(userId));

        var pendingItems = await _parsedTaskItemRepository.GetPendingByUserIdAsync(userId, cancellationToken);

        return pendingItems.Select(item => new ParsedTaskItemResponse(
            item.Id,
            item.VoiceEntryId,
            item.UserId,
            item.Title,
            item.Category,
            item.Priority,
            item.DueDate,
            item.ConfidenceScore,
            item.Status,
            item.CreatedAt));
    }
}
