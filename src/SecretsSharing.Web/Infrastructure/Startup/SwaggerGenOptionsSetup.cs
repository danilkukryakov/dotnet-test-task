using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SecretsSharing.Web.Infrastructure.Startup;

/// <summary>
/// SwaggerGen options.
/// </summary>
internal class SwaggerGenOptionsSetup
{
    /// <summary>
    /// Setup.
    /// </summary>
    /// <param name="options">Swagger generation options.</param>
    public void Setup(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Secrets Sharing App",
            Description = "API documentation for the project.",
        });

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Insert JWT token to the field.",
            Scheme = "bearer",
            BearerFormat = "JWT",
            Name = "bearer",
            Type = SecuritySchemeType.Http
        });

        // XML comments.
        options.IncludeXmlComments(GetAssemblyLocationByType(GetType()));

        // Operation filters.
        options.SchemaFilter<SwaggerEnumDescriptionSchemaOperationFilter>();
        options.OperationFilter<SwaggerEnumDescriptionSchemaOperationFilter>();
        options.OperationFilter<SwaggerSecurityRequirementsOperationFilter>();
    }

    private static string GetAssemblyLocationByType(Type type) =>
        Path.Combine(AppContext.BaseDirectory, $"{type.Assembly.GetName().Name}.xml");
}
