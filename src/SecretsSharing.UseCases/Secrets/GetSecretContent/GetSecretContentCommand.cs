using MediatR;

namespace SecretsSharing.UseCases.Secrets.GetSecretContent;

/// <summary>
/// Get Secret file content command.
/// </summary>
public record GetSecretContentCommand : IRequest<GetSecretContentCommandResult>
{
    /// <summary>
    /// Secret link id.
    /// </summary>
    public required Guid SecretLinkId { get; init; }
}
