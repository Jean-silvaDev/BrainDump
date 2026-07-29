namespace BrainDump.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Secret { get; set; } = "SuperSecretKeyBrainDumpAppProject2026_Minimum32CharsLength!";
    public string Issuer { get; set; } = "BrainDump";
    public string Audience { get; set; } = "BrainDumpAppUsers";
    public int ExpiryMinutes { get; set; } = 15;
    public int RefreshTokenExpiryDays { get; set; } = 7;
}
