using BrainDump.Domain.Entities;

namespace BrainDump.Application.DTOs.Tasks;

/// <summary>
/// DTO de retorno para tarefas oficiais confirmadas.
/// </summary>
public record TaskItemResponse(
    Guid Id,
    Guid UserId,
    string Title,
    Category Category,
    Priority Priority,
    DateTime? DueDate,
    bool IsCompleted,
    DateTime? CompletedAt,
    DateTime CreatedAt);
