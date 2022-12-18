using SecretsSharing.Infrastructure.Abstractions;

namespace SecretsSharing.Web.Infrastructure.Web;

/// <summary>
/// Logged user accessor implementation.
/// </summary>
internal class CurrentUserAccessor : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    public int GetCurrentUserId()
    {
        if (httpContextAccessor.HttpContext == null)
        {
            throw new InvalidOperationException("There is no active HTTP context specified.");
        }

        return httpContextAccessor.HttpContext.User.GetCurrentUserId();
    }

    /// <inheritdoc />
    public bool IsAuthenticated()
    {
        if (httpContextAccessor.HttpContext == null)
        {
            return false;
        }
        return httpContextAccessor.HttpContext.User.TryGetCurrentUserId(out _);
    }
}
