using SecretsSharing.Domain.Entities;

namespace SecretsSharing.UseCases.Secrets.GetSecretContent;

/// <summary>
/// Get Secret file command result.
/// </summary>
public class GetSecretContentCommandResult
{
    /// <summary>
    /// Link id to text secret.
    /// </summary>
    public required Guid LinkId { get; init; }

    /// <summary>
    /// Content of secret file.
    /// It will be different according to SecretType.
    /// </summary>
    public required string Content { get; init; }

    /// <summary>
    /// Secret file content type.
    /// </summary>
    public SecretType SecretType { get; init; }
}
