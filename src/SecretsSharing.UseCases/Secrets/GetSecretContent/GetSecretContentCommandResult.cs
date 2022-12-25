using Microsoft.AspNetCore.Mvc;
using SecretsSharing.Domain.Entities;

namespace SecretsSharing.UseCases.Secrets.GetSecretContent;

/// <summary>
/// Get Secret file command result.
/// </summary>
public class GetSecretContentCommandResult
{
    /// <summary>
    /// Link id to text secret.
    /// </summary>
    public required Guid LinkId { get; init; }

    /// <summary>
    /// Content of Text secret file.
    /// </summary>
    public string? TextContent { get; set; }

    /// <summary>
    /// Secret file content type.
    /// </summary>
    public SecretType SecretType { get; init; }

    /// <summary>
    /// File stream (only for 'File' secret type).
    /// </summary>
    public FileStreamResult? FileStream { get; set; }
}
