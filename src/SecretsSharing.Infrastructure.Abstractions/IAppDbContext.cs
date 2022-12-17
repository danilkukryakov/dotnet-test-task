using Microsoft.EntityFrameworkCore;
using SecretsSharing.Domain.Entities;

namespace SecretsSharing.Infrastructure.Abstractions;

/// <summary>
/// Abstraction of application DB context unit of work.
/// </summary>
public interface IAppDbContext : IDisposable
{
    /// <summary>
    /// Users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Links.
    /// </summary>
    DbSet<Link> Links { get; }

    /// <summary>
    /// Secret files.
    /// </summary>
    DbSet<SecretFile> SecretFiles { get; }

    /// <summary>
    /// Secret files.
    /// </summary>
    DbSet<SecretText> SecretTexts { get; }
}
