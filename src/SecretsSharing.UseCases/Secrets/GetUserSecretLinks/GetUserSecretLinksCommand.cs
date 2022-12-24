using MediatR;

namespace SecretsSharing.UseCases.Secrets.GetUserSecretLinks;

/// <summary>
/// Get Secret links for user command.
/// </summary>
public record GetUserSecretLinksCommand : IRequest<GetUserSecretLinksCommandResult>
{
}
