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
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// MIME type for file.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string MimeType { get; set; } = string.Empty;

    /// <summary>
    /// Reference to file at storage.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string BlobRef { get; set; } = string.Empty;
}
