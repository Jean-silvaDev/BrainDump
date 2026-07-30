using BrainDump.Domain.Entities;

namespace BrainDump.Domain.Repositories;

/// <summary>
/// Interface de repositório para acesso aos itens de tarefa sugeridos/parsed.
/// </summary>
public interface IParsedTaskItemRepository
{
    Task<ParsedTaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ParsedTaskItem>> GetByVoiceEntryIdAsync(Guid voiceEntryId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ParsedTaskItem>> GetPendingByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<ParsedTaskItem> items, CancellationToken cancellationToken = default);
    Task UpdateAsync(ParsedTaskItem item, CancellationToken cancellationToken = default);
}
