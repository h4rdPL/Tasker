namespace Tasker.Domain.Entities;

public class JwtOptions
{
    public string Key { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int ExpiresMinutes { get; init; }
}
