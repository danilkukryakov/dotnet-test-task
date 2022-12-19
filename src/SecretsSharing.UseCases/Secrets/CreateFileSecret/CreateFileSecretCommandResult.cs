namespace SecretsSharing.UseCases.Secrets.CreateFileSecret;

/// <summary>
/// Create FileSecret command result.
/// </summary>
public class CreateFileSecretCommandResult
{
    /// <summary>
    /// Link id to file secret.
    /// </summary>
    public required Guid LinkId { get; init; }

    // TODO: Probably a full link DTO should be returned.
}
