namespace SecretsSharing.UseCases.Users;

/// <summary>
/// API generated token model.
/// </summary>
public class TokenModel
{
    /// <summary>
    /// Token.
    /// </summary>
    public required string Token { get; init; }

    /// <summary>
    /// Token expiration in seconds.
    /// </summary>
    public long ExpiresIn { get; init; }
}
