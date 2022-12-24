using MediatR;

namespace SecretsSharing.UseCases.Secrets.DeleteSecret;

/// <summary>
/// Delete Secret command.
/// </summary>
public record DeleteSecretCommand : IRequest
{
    /// <summary>
    /// Secret link ID.
    /// </summary>
    public required Guid SecretLinkId { get; init; }
}
