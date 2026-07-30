using BrainDump.Domain.Entities;

namespace BrainDump.Domain.Repositories;

/// <summary>
/// Contrato de repositório para persistência de gravações de voz.
/// </summary>
public interface IVoiceEntryRepository
{
    Task<VoiceEntry?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<VoiceEntry>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(VoiceEntry voiceEntry, CancellationToken cancellationToken = default);
    Task UpdateAsync(VoiceEntry voiceEntry, CancellationToken cancellationToken = default);
}
