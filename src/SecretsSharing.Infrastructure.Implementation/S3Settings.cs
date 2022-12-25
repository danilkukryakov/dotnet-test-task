using System.ComponentModel.DataAnnotations;

namespace SecretsSharing.Infrastructure.Implementation;

/// <summary>
/// S3 settings.
/// </summary>
public class S3Settings
{
    /// <summary>
    /// Access key. Required for minio only.
    /// </summary>
    public string? AccessKey { get; set; }

    /// <summary>
    /// Secret key. Required for minio only.
    /// </summary>
    public string? SecretKey { get; set; }

    /// <summary>
    /// Region name.
    /// </summary>
    [Required]
    public required string RegionName { get; set; }

    /// <summary>
    /// Bucket name.
    /// </summary>
    [Required]
    public required string BucketName { get; set; }

    /// <summary>
    /// Optional service URL. Required for minio only.
    /// </summary>
    public string? ServiceUrl { get; set; }

    /// <summary>
    /// True is required if minio is used.
    /// </summary>
    public bool ForcePathStyle { get; set; }
}
