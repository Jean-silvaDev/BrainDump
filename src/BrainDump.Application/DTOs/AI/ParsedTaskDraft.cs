using BrainDump.Domain.Entities;

namespace BrainDump.Application.DTOs.AI;

/// <summary>
/// DTO rascunho de tarefa extraída do texto via LLM.
/// </summary>
public record ParsedTaskDraft(
    string Title,
    Category Category,
    Priority Priority,
    DateTime? DueDate,
    float ConfidenceScore = 1.0f);
