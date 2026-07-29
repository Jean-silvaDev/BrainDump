namespace BrainDump.Application.DTOs.Auth;

/// <summary>
/// DTO de resposta para operações de autenticação bem-sucedidas.
/// </summary>
public record AuthResponse(
    Guid Id,
    string Name,
    string Email,
    string AccessToken,
    string RefreshToken
);
