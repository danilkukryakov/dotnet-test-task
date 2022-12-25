using MediatR;
using Microsoft.AspNetCore.Identity;
using SecretsSharing.Domain.Entities;
using SecretsSharing.Domain.Exceptions;
using SecretsSharing.Infrastructure.Abstractions;

namespace SecretsSharing.UseCases.Users.RegisterUser;

/// <summary>
/// Handler for <see cref="RegisterUserCommand"/>.
/// </summary>
internal class RegisterUserCommandHandler : AsyncRequestHandler<RegisterUserCommand>
{
    private readonly IAppDbContext dbContext;
    private readonly UserManager<User> userManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    public RegisterUserCommandHandler(IAppDbContext dbContext,
        UserManager<User> userManager)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
    }

    /// <inheritdoc/>
    protected override async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            UserName = request.RegisterDto.Email,
            Email = request.RegisterDto.Email,
            EmailConfirmed = true,
        };
        var result = await userManager.CreateAsync(newUser, request.RegisterDto.Password);

        if (!result.Succeeded)
        {
            throw new DomainException("Can not register new user.");
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
