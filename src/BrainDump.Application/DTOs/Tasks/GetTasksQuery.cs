using BrainDump.Domain.Entities;

namespace BrainDump.Application.DTOs.Tasks;

/// <summary>
/// DTO de consulta para filtragem de tarefas do usuário.
/// </summary>
public record GetTasksQuery(
    Category? Category = null,
    Priority? Priority = null,
    bool? IsCompleted = null,
    string? SearchTerm = null);
