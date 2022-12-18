namespace SecretsSharing.UseCases.Users;

/// <summary>
/// Shared constants for authentication.
/// </summary>
public static class AuthenticationConstants
{
    /// <summary>
    /// Issued at date/time claim. https://tools.ietf.org/html/rfc7519#page-10 .
    /// </summary>
    public const string IatClaimType = "iat";

    /// <summary>
    /// Access token expiration time.
    /// </summary>
    public static readonly TimeSpan AccessTokenExpirationTime = TimeSpan.FromHours(3);
}
