using BrainDump.Domain.Entities;

namespace BrainDump.Application.Common.Interfaces;

/// <summary>
/// Contrato para geração de Access Tokens (JWT) e Refresh Tokens.
/// </summary>
public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);
    (string RefreshToken, DateTime ExpiryTime) GenerateRefreshToken();
    Guid? GetUserIdFromExpiredToken(string token);
}
