using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.UseCases.Users;
using SecretsSharing.UseCases.Users.LoginUser;
using SecretsSharing.UseCases.Users.RegisterUser;

namespace SecretsSharing.Web.Controllers;

/// <summary>
/// Authentication controller.
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Register user.
    /// </summary>
    /// <param name="register">DTO for user registration.</param>
    /// <param name="cancellationToken">Token to monitor request cancellation.</param>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task Register([Required] RegisterUserDto register, CancellationToken cancellationToken)
        => await mediator.Send(new RegisterUserCommand { RegisterDto = register }, cancellationToken);

    /// <summary>
    /// Authenticate user by email and password.
    /// </summary>
    /// <param name="loginUser">Login dto.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<TokenModel> Login([Required] LoginUserDto loginUser, CancellationToken cancellationToken)
        => (await mediator.Send(new LoginUserCommand { LoginUser = loginUser }, cancellationToken)).TokenModel;
}
