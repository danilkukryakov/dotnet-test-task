namespace SecretsSharing.UseCases.Secrets.CreateTextSecret;

/// <summary>
/// Create TextSecret command result.
/// </summary>
public class CreateTextSecretCommandResult
{
    /// <summary>
    /// Link id to text secret.
    /// </summary>
    public required Guid LinkId { get; init; }

    // TODO: Probably a full link DTO should be returned.
}
