using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecretsSharing.Infrastructure.Abstractions;
using SecretsSharing.Domain.Entities;

namespace SecretsSharing.Infrastructure.DataAccess;

/// <summary>
/// Application DB context.
/// </summary>
public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IAppDbContext
{
    /// <inheritdoc/>
    public DbSet<Link> Links => Set<Link>();

    /// <inheritdoc/>
    public DbSet<SecretFile> SecretFiles => Set<SecretFile>();

    /// <inheritdoc/>
    public DbSet<SecretText> SecretTexts => Set<SecretText>();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">Options for <see cref="DbContext" />.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<User>()
            .HasMany(user => user.Links)
            .WithOne();
    }
}
