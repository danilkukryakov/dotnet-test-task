using MediatR;
using Microsoft.AspNetCore.Identity;
using SecretsSharing.Domain.Entities;
using SecretsSharing.Infrastructure.Abstractions;

namespace SecretsSharing.UseCases.Users.LoginUser;

/// <summary>
/// Handler for <see cref="LoginUserCommand" />.
/// </summary>
internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResult>
{
    private readonly SignInManager<User> signInManager;
    private readonly IAuthenticationTokenService tokenService;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="signInManager">Sign in manager.</param>
    /// <param name="tokenService">Token service.</param>
    public LoginUserCommandHandler(
        SignInManager<User> signInManager,
        IAuthenticationTokenService tokenService)
    {
        this.signInManager = signInManager;
        this.tokenService = tokenService;
    }

    /// <inheritdoc />
    public async Task<LoginUserCommandResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Password sign in.
        var result = await signInManager.PasswordSignInAsync(request.LoginUser.Email, request.LoginUser.Password,
            isPersistent: false, lockoutOnFailure: false);
        ValidateSignInResult(result, request.LoginUser.Email);

        // Get user and log.
        var user = await signInManager.UserManager.FindByEmailAsync(request.LoginUser.Email);
        if (user == null)
        {
            throw new Exception("Email or password is incorrect.");
        }

        // Combine refresh token with user id.
        var principal = await signInManager.CreateUserPrincipalAsync(user);

        // Give token.
        return new LoginUserCommandResult
        {
            UserId = user.Id,
            TokenModel = TokenModelGenerator.Generate(tokenService, principal.Claims)
        };
    }

    private void ValidateSignInResult(SignInResult signInResult, string email)
    {
        if (!signInResult.Succeeded)
        {
            if (signInResult.IsNotAllowed)
            {
                throw new Exception($"User {email} is not allowed to Sign In.");
            }
            if (signInResult.IsLockedOut)
            {
                throw new Exception($"User {email} is locked out.");
            }
            throw new Exception("Email or password is incorrect.");
        }
    }
}
