using MediatR;
using SecretsSharing.UseCases.Users.LoginUser;

namespace SecretsSharing.Web.Infrastructure.DependencyInjection;

/// <summary>
/// Register Mediator as dependency.
/// </summary>
internal static class MediatRModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddMediatR(typeof(LoginUserCommand).Assembly);
    }
}
