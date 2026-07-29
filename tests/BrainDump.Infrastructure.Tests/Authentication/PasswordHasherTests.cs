using BrainDump.Infrastructure.Authentication;
using Xunit;

namespace BrainDump.Infrastructure.Tests.Authentication;

public class PasswordHasherTests
{
    private readonly PasswordHasher _passwordHasher = new();

    [Fact]
    public void HashPassword_WhenPlainPasswordProvided_ReturnsValidHash()
    {
        // Arrange
        var password = "SuperSecretPassword123!";

        // Act
        var hash = _passwordHasher.HashPassword(password);

        // Assert
        Assert.NotNull(hash);
        Assert.NotEqual(password, hash);
    }

    [Fact]
    public void VerifyPassword_WhenPasswordMatches_ReturnsTrue()
    {
        // Arrange
        var password = "SuperSecretPassword123!";
        var hash = _passwordHasher.HashPassword(password);

        // Act
        var result = _passwordHasher.VerifyPassword(password, hash);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_WhenPasswordDoesNotMatch_ReturnsFalse()
    {
        // Arrange
        var password = "SuperSecretPassword123!";
        var wrongPassword = "WrongPassword123!";
        var hash = _passwordHasher.HashPassword(password);

        // Act
        var result = _passwordHasher.VerifyPassword(wrongPassword, hash);

        // Assert
        Assert.False(result);
    }
}
