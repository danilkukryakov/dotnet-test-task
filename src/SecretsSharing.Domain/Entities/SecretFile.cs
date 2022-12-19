using System.ComponentModel.DataAnnotations;

namespace SecretsSharing.Domain.Entities;

/// <summary>
/// Secret file.
/// </summary>
public class SecretFile
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Secret file name.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public required string Name { get; set; }

    /// <summary>
    /// MIME type for file.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public required string MimeType { get; set; }

    /// <summary>
    /// Reference to file at storage.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public required string BlobRef { get; set; }
}
