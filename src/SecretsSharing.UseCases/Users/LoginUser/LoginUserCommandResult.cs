namespace SecretsSharing.UseCases.Users.LoginUser;

/// <summary>
/// User login command result.
/// </summary>
public class LoginUserCommandResult
{
    /// <summary>
    /// Logged user id (if success).
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    /// New refresh token.
    /// </summary>
    public required TokenModel TokenModel { get; init; }
}
