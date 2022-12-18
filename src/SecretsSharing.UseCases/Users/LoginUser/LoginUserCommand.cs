using MediatR;

namespace SecretsSharing.UseCases.Users.LoginUser;

/// <summary>
/// Login user command.
/// </summary>
public record LoginUserCommand : IRequest<LoginUserCommandResult>
{
    /// <summary>
    /// DTO for login use case.
    /// </summary>
    public required LoginUserDto LoginUser { get; init; }
}
