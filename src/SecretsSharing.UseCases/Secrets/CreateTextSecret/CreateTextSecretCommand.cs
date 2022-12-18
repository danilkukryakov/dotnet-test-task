using MediatR;

namespace SecretsSharing.UseCases.Secrets.CreateTextSecret;

/// <summary>
/// Create Text Secret command.
/// </summary>
public record CreateTextSecretCommand : IRequest<CreateTextSecretCommandResult>
{
    /// <summary>
    /// DTO for login use case.
    /// </summary>
    public required CreateTextSecretDto TextSecretDto { get; init; }
}
