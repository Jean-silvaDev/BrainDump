namespace BrainDump.Application.DTOs.Review;

/// <summary>
/// DTO de requisição para confirmação e conversão de rascunhos em tarefas oficiais.
/// Se ParsedItemIds for nulo ou vazio, confirma todos os itens pendentes do usuário.
/// </summary>
public record ConfirmTasksRequest(
    IEnumerable<Guid>? ParsedItemIds);
