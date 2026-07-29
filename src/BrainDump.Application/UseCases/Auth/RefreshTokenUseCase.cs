using BrainDump.Application.Common.Interfaces;
using BrainDump.Application.DTOs.Auth;

namespace BrainDump.Application.UseCases.Auth;

/// <summary>
/// Caso de uso responsável pela renovação (rotação) de Refresh Tokens.
/// </summary>
public class RefreshTokenUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RefreshTokenUseCase(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> ExecuteAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
        var userId = _jwtTokenGenerator.GetUserIdFromExpiredToken(request.AccessToken);
        if (userId == null)
        {
            throw new UnauthorizedAccessException("Access Token inválido.");
        }

        var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);
        if (user == null || !user.IsRefreshTokenValid(request.RefreshToken))
        {
            throw new UnauthorizedAccessException("Refresh Token inválido ou expirado.");
        }

        var newAccessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var (newRefreshToken, expiry) = _jwtTokenGenerator.GenerateRefreshToken();

        user.SetRefreshToken(newRefreshToken, expiry);
        await _userRepository.UpdateAsync(user, cancellationToken);

        return new AuthResponse(user.Id, user.Name, user.Email, newAccessToken, newRefreshToken);
    }
}
