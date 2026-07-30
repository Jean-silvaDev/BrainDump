using BrainDump.Domain.Entities;

namespace BrainDump.Application.DTOs.Review;

/// <summary>
/// DTO para atualização de um rascunho de tarefa na tela de revisão.
/// </summary>
public record UpdateParsedTaskItemRequest(
    string Title,
    Category Category,
    Priority Priority,
    DateTime? DueDate);
