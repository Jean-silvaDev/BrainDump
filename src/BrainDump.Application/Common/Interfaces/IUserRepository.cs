using BrainDump.Domain.Entities;

namespace BrainDump.Application.Common.Interfaces;

/// <summary>
/// Contrato do repositório para persistência e consulta de usuários.
/// </summary>
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
}
