using BrainDump.Domain.Entities;

namespace BrainDump.Application.DTOs.Tasks;

/// <summary>
/// DTO de requisição para edição de dados de uma tarefa oficial.
/// </summary>
public record EditTaskRequest(
    string Title,
    Category Category,
    Priority Priority,
    DateTime? DueDate);
