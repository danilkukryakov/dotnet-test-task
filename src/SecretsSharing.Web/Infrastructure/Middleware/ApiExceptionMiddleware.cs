using SecretsSharing.Domain.Exceptions;

namespace SecretsSharing.Web.Infrastructure.Middleware;

/// <summary>
/// Exception handling middleware. In general:
/// DomainException => 400.
/// _ => 500 with stack trace.
/// </summary>
internal sealed class ApiExceptionMiddleware
{
    private const string ProblemMimeType = "text/plain";

    private readonly RequestDelegate next;
    private readonly ILogger<ApiExceptionMiddleware> logger;

    private static readonly IDictionary<Type, int> ExceptionStatusCodes = new Dictionary<Type, int>
    {
        [typeof(DomainException)] = StatusCodes.Status400BadRequest,
        [typeof(NotFoundException)] = StatusCodes.Status404NotFound,
        [typeof(NotImplementedException)] = StatusCodes.Status501NotImplemented,
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiExceptionMiddleware" /> class.
    /// </summary>
    public ApiExceptionMiddleware(
        RequestDelegate next,
        ILogger<ApiExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    /// <summary>
    /// Invokes the next middleware.
    /// </summary>
    /// <param name="httpContext">HTTP context.</param>
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception exception)
        {
            if (httpContext.Response.HasStarted)
            {
                logger.LogWarning(
                    "The response has already started, the API exception middleware will not be executed.");
                throw;
            }
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = GetStatusCodeByExceptionType(exception.GetType());
            httpContext.Response.ContentType = ProblemMimeType;
            await httpContext.Response.WriteAsync(exception.Message);
        }
    }

    private static int GetStatusCodeByExceptionType(Type exceptionType)
    {
        // Most probable case.
        if (exceptionType == typeof(DomainException))
        {
            return StatusCodes.Status400BadRequest;
        }
        foreach ((Type exceptionTypeKey, int statusCode) in ExceptionStatusCodes)
        {
            if (exceptionTypeKey.IsAssignableFrom(exceptionType))
            {
                return statusCode;
            }
        }
        return StatusCodes.Status500InternalServerError;
    }
}
