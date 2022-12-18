using SecretsSharing.Infrastructure.Abstractions;
using SecretsSharing.Infrastructure.DataAccess;
using SecretsSharing.Web.Infrastructure.Jwt;

namespace SecretsSharing.Web.Infrastructure.DependencyInjection;

/// <summary>
/// System specific dependencies.
/// </summary>
internal static class SystemModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationTokenService, SystemJwtTokenService>();
        services.AddScoped<IAppDbContext, AppDbContext>();
    }
}
