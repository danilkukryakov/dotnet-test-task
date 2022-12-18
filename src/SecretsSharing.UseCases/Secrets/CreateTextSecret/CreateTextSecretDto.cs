namespace SecretsSharing.UseCases.Secrets.CreateTextSecret;

/// <summary>
/// DTO for create TextSecret use case.
/// </summary>
public class CreateTextSecretDto
{
    /// <summary>
    /// Secret content.
    /// </summary>
    public required string Content { get; init; }

    /// <summary>
    /// Should file be deleted after download.
    /// </summary>
    public bool DeleteAfterDownload { get; set; }
}
