using SecretsSharing.Domain.Entities;

namespace SecretsSharing.UseCases.Secrets.GetUserSecretLinks;

/// <summary>
/// User secret link DTO.
/// </summary>
public class UserSecretLinkDto
{
    /// <summary>
    /// Link id.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Secret type.
    /// </summary>
    public SecretType SecretType { get; init; }
}
