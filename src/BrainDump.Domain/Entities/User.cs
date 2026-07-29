namespace BrainDump.Domain.Entities;

/// <summary>
/// Representa o usuário no sistema, contendo regras de negócio de autenticação e identificação.
/// </summary>
public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiryTime { get; private set; }

    private User()
    {
        Name = string.Empty;
        Email = string.Empty;
        PasswordHash = string.Empty;
    }

    public User(Guid id, string name, string email, string passwordHash, DateTime createdAt)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("O ID do usuário não pode ser vazio.", nameof(id));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do usuário não pode ser vazio.", nameof(name));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Formato de e-mail inválido.", nameof(email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("O hash da senha não pode ser vazio.", nameof(passwordHash));

        Id = id;
        Name = name.Trim();
        Email = email.Trim().ToLowerInvariant();
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }

    public static User Create(string name, string email, string passwordHash)
    {
        return new User(Guid.NewGuid(), name, email, passwordHash, DateTime.UtcNow);
    }

    public void SetRefreshToken(string refreshToken, DateTime expiryTime)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            throw new ArgumentException("O refresh token não pode ser vazio.", nameof(refreshToken));

        if (expiryTime <= DateTime.UtcNow)
            throw new ArgumentException("A data de expiração do refresh token deve ser no futuro.", nameof(expiryTime));

        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = expiryTime;
    }

    public void RevokeRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiryTime = null;
    }

    public bool IsRefreshTokenValid(string refreshToken)
    {
        return RefreshToken == refreshToken && RefreshTokenExpiryTime.HasValue && RefreshTokenExpiryTime.Value > DateTime.UtcNow;
    }
}
