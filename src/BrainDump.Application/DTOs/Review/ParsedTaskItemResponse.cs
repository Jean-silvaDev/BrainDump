using BrainDump.Domain.Entities;

namespace BrainDump.Application.DTOs.Review;

/// <summary>
/// DTO de retorno para rascunhos de tarefas pendentes de revisão.
/// </summary>
public record ParsedTaskItemResponse(
    Guid Id,
    Guid VoiceEntryId,
    Guid UserId,
    string Title,
    Category Category,
    Priority Priority,
    DateTime? DueDate,
    float ConfidenceScore,
    ParsedTaskStatus Status,
    DateTime CreatedAt);
