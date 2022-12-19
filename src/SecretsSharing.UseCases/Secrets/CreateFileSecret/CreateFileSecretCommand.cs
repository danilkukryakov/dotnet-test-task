using MediatR;

namespace SecretsSharing.UseCases.Secrets.CreateFileSecret;

/// <summary>
/// Create Text Secret command.
/// </summary>
public record CreateFileSecretCommand : IRequest<CreateFileSecretCommandResult>
{
    /// <summary>
    /// DTO for file secret create use case.
    /// </summary>
    public required CreateFileSecretDto FileSecretDto { get; init; }
}
