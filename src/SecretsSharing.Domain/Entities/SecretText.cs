using System.ComponentModel.DataAnnotations;

namespace SecretsSharing.Domain.Entities;

/// <summary>
/// Secret text.
/// </summary>
public class SecretText
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Content of secret text.
    /// </summary>
    [Required]
    public string Content { get; set; } = string.Empty;
}
