using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SecretsSharing.Web.Infrastructure.Startup;

/// <summary>
/// Automatically adds information about authorization requirements for API endpoints.
/// </summary>
internal sealed class SwaggerSecurityRequirementsOperationFilter : IOperationFilter
{
    private static readonly string UnauthorizedCode = StatusCodes.Status401Unauthorized.ToString();

    private static readonly OpenApiSecurityRequirement BearerSecurityRequirement = new()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        },
    };

    /// <inheritdoc />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!DoesActionRequireAuthorization(context.MethodInfo))
        {
            return;
        }

        operation.Responses.Add(UnauthorizedCode, new OpenApiResponse { Description = "Unauthorized" });
        operation.Security.Add(BearerSecurityRequirement);
    }

    private static bool DoesActionRequireAuthorization(MethodInfo methodInfo)
    {
        if (methodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
        {
            return false;
        }

        return methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), true).Any() ||
            (methodInfo.ReflectedType?.GetCustomAttributes(typeof(AuthorizeAttribute), true).Any() ?? false);
    }
}
