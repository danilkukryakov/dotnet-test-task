using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SecretsSharing.UseCases.Secrets.CreateFileSecret;

/// <summary>
/// DTO for upload of file secret.
/// /// </summary>
public class CreateFileSecretDto
{
    /// <summary>
    /// File to upload.
    /// </summary>
    [Required]
    public required IFormFile File { get; init; }

    /// <summary>
    /// Should file be deleted after download.
    /// </summary>
    public bool DeleteAfterDownload { get; set; }
}
