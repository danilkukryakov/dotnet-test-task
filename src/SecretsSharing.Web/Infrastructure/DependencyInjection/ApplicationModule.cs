using SecretsSharing.Infrastructure.Abstractions;
using SecretsSharing.Infrastructure.Implementation;

namespace SecretsSharing.Web.Infrastructure.DependencyInjection;

/// <summary>
/// Application specific dependencies.
/// </summary>
internal static class ApplicationModule
{
    /// <summary>
    /// Register dependencies.
    /// </summary>
    /// <param name="services">Services.</param>
    /// <param name="configuration">Configuration.</param>
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(provider =>
        {
            var s3Settings = configuration.GetSection("S3Settings").Get<S3Settings>();
            if (s3Settings == null) {
                throw new Exception("S3Setting was not provided.");
            }
            var logger = provider.GetService<ILogger<S3FileStorage>>();
            var keyGenStrategy = new GuidKeyGenerationStrategy();
            return new S3FileStorage(s3Settings, keyGenStrategy);
        });
        services.AddTransient<IBlobStorage>(provider => provider.GetRequiredService<S3FileStorage>());
    }
}
