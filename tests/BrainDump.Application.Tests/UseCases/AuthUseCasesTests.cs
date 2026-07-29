using BrainDump.Application.Common.Interfaces;
using BrainDump.Application.DTOs.Auth;
using BrainDump.Application.UseCases.Auth;
using BrainDump.Domain.Entities;
using NSubstitute;
using Xunit;

namespace BrainDump.Application.Tests.UseCases;

public class AuthUseCasesTests
{
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IPasswordHasher _passwordHasher = Substitute.For<IPasswordHasher>();
    private readonly IJwtTokenGenerator _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();

    [Fact]
    public async Task Register_WhenEmailIsNew_CreatesUserAndReturnsAuthResponse()
    {
        // Arrange
        var request = new RegisterRequest("Jean", "jean@example.com", "password123");
        _userRepository.GetByEmailAsync(request.Email, Arg.Any<CancellationToken>()).Returns((User?)null);
        _passwordHasher.HashPassword(request.Password).Returns("hashed_password");
        _jwtTokenGenerator.GenerateAccessToken(Arg.Any<User>()).Returns("access_token");
        _jwtTokenGenerator.GenerateRefreshToken().Returns(("refresh_token", DateTime.UtcNow.AddDays(7)));

        var useCase = new RegisterUserUseCase(_userRepository, _passwordHasher, _jwtTokenGenerator);

        // Act
        var response = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("Jean", response.Name);
        Assert.Equal("jean@example.com", response.Email);
        Assert.Equal("access_token", response.AccessToken);
        Assert.Equal("refresh_token", response.RefreshToken);
        await _userRepository.Received(1).AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Register_WhenEmailAlreadyExists_ThrowsInvalidOperationException()
    {
        // Arrange
        var request = new RegisterRequest("Jean", "jean@example.com", "password123");
        var existingUser = User.Create("Jean", "jean@example.com", "hashed");
        _userRepository.GetByEmailAsync(request.Email, Arg.Any<CancellationToken>()).Returns(existingUser);

        var useCase = new RegisterUserUseCase(_userRepository, _passwordHasher, _jwtTokenGenerator);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ExecuteAsync(request));
    }

    [Fact]
    public async Task Login_WhenCredentialsAreValid_ReturnsAuthResponse()
    {
        // Arrange
        var request = new LoginRequest("jean@example.com", "password123");
        var user = User.Create("Jean", "jean@example.com", "hashed_password");
        _userRepository.GetByEmailAsync(request.Email, Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.VerifyPassword(request.Password, user.PasswordHash).Returns(true);
        _jwtTokenGenerator.GenerateAccessToken(user).Returns("access_token");
        _jwtTokenGenerator.GenerateRefreshToken().Returns(("new_refresh_token", DateTime.UtcNow.AddDays(7)));

        var useCase = new LoginUseCase(_userRepository, _passwordHasher, _jwtTokenGenerator);

        // Act
        var response = await useCase.ExecuteAsync(request);

        // Assert
        Assert.Equal("access_token", response.AccessToken);
        Assert.Equal("new_refresh_token", response.RefreshToken);
        await _userRepository.Received(1).UpdateAsync(user, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Login_WhenInvalidCredentials_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var request = new LoginRequest("jean@example.com", "wrong_password");
        var user = User.Create("Jean", "jean@example.com", "hashed_password");
        _userRepository.GetByEmailAsync(request.Email, Arg.Any<CancellationToken>()).Returns(user);
        _passwordHasher.VerifyPassword(request.Password, user.PasswordHash).Returns(false);

        var useCase = new LoginUseCase(_userRepository, _passwordHasher, _jwtTokenGenerator);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => useCase.ExecuteAsync(request));
    }
}
