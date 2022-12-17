namespace SecretsSharing.Domain.Entities;

// TODO: Add user identity provider.
// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=netcore-cli

/// <summary>
/// TODO: Remove after migration to identity provider.
/// </summary>
public class IdentityUser
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// App user entity.
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// User secret links. Many to many relationship table.
    /// </summary>
    public ICollection<Link> Links { get; } = new List<Link>();
}
