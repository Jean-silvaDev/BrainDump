using BrainDump.Application.Common.Interfaces;
using BrainDump.Application.DTOs.Auth;

namespace BrainDump.Application.UseCases.Auth;

/// <summary>
/// Caso de uso responsável pelo login de usuários e emissão de tokens JWT.
/// </summary>
public class LoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> ExecuteAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
        }

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var (refreshToken, expiry) = _jwtTokenGenerator.GenerateRefreshToken();

        user.SetRefreshToken(refreshToken, expiry);
        await _userRepository.UpdateAsync(user, cancellationToken);

        return new AuthResponse(user.Id, user.Name, user.Email, accessToken, refreshToken);
    }
}
