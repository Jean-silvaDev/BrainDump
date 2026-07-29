using BrainDump.Application.Common.Interfaces;
using BrainDump.Application.DTOs.Auth;
using BrainDump.Domain.Entities;

namespace BrainDump.Application.UseCases.Auth;

/// <summary>
/// Caso de uso responsável pelo registro de novos usuários.
/// </summary>
public class RegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterUserUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> ExecuteAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Já existe um usuário cadastrado com este e-mail.");
        }

        var passwordHash = _passwordHasher.HashPassword(request.Password);
        var user = User.Create(request.Name, request.Email, passwordHash);

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var (refreshToken, expiry) = _jwtTokenGenerator.GenerateRefreshToken();

        user.SetRefreshToken(refreshToken, expiry);
        await _userRepository.AddAsync(user, cancellationToken);

        return new AuthResponse(user.Id, user.Name, user.Email, accessToken, refreshToken);
    }
}
