using BrainDump.Application.Common.Interfaces;

namespace BrainDump.Infrastructure.Authentication;

/// <summary>
/// Implementação de hashing de senhas utilizando BCrypt.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
