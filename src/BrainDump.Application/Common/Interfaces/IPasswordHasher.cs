namespace BrainDump.Application.Common.Interfaces;

/// <summary>
/// Contrato para hash e verificação segura de senhas de usuários.
/// </summary>
public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}
