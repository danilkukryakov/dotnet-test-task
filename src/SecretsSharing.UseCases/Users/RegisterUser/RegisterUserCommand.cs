using MediatR;

namespace SecretsSharing.UseCases.Users.RegisterUser;

/// <summary>
/// Register command.
/// </summary>
public class RegisterUserCommand : IRequest
{
    /// <summary>
    /// Register DTO.
    /// </summary>
    public required RegisterUserDto RegisterDto { get; init; }
}
