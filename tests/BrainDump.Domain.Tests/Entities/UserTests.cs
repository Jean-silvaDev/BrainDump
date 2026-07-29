using BrainDump.Domain.Entities;
using Xunit;

namespace BrainDump.Domain.Tests.Entities;

public class UserTests
{
    [Fact]
    public void Create_WhenValidDataProvided_ReturnsNewUser()
    {
        // Arrange
        var name = "Jean Silva";
        var email = "jean@example.com";
        var passwordHash = "hashed_secret_password";

        // Act
        var user = User.Create(name, email, passwordHash);

        // Assert
        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal("Jean Silva", user.Name);
        Assert.Equal("jean@example.com", user.Email);
        Assert.Equal(passwordHash, user.PasswordHash);
    }

    [Theory]
    [InlineData("", "jean@example.com", "hash")]
    [InlineData("Jean", "", "hash")]
    [InlineData("Jean", "invalid-email", "hash")]
    [InlineData("Jean", "jean@example.com", "")]
    public void Create_WhenInvalidDataProvided_ThrowsArgumentException(string name, string email, string passwordHash)
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => User.Create(name, email, passwordHash));
    }

    [Fact]
    public void SetRefreshToken_WhenValidTokenAndFutureExpiry_SetsRefreshTokenSuccessfully()
    {
        // Arrange
        var user = User.Create("Jean", "jean@example.com", "hash");
        var refreshToken = "sample_refresh_token";
        var expiry = DateTime.UtcNow.AddDays(7);

        // Act
        user.SetRefreshToken(refreshToken, expiry);

        // Assert
        Assert.Equal(refreshToken, user.RefreshToken);
        Assert.Equal(expiry, user.RefreshTokenExpiryTime);
        Assert.True(user.IsRefreshTokenValid(refreshToken));
    }

    [Fact]
    public void IsRefreshTokenValid_WhenTokenExpired_ReturnsFalse()
    {
        // Arrange
        var user = User.Create("Jean", "jean@example.com", "hash");
        var refreshToken = "sample_refresh_token";
        var expiry = DateTime.UtcNow.AddSeconds(-10);

        // Act & Assert
        Assert.False(user.IsRefreshTokenValid(refreshToken));
    }
}
