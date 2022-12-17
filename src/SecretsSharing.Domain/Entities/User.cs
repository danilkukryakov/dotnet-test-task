
using Microsoft.AspNetCore.Identity;

namespace SecretsSharing.Domain.Entities;

/// <summary>
/// App user entity.
/// </summary>
public class User : IdentityUser<int>
{
    /// <summary>
    /// User secret links. Many to many relationship table.
    /// </summary>
    public ICollection<Link> Links { get; } = new List<Link>();
}
