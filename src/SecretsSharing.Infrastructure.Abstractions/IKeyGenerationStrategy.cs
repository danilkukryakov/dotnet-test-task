namespace SecretsSharing.Infrastructure.Abstractions;

/// <summary>
/// Interface for key generation strategy.
/// </summary>
public interface IKeyGenerationStrategy
{
    /// <summary>
    /// Generate BLOB key.
    /// </summary>
    /// <param name="mimeType">BLOB MIME type.</param>
    /// <param name="path">Custom path in key.</param>
    /// <returns>BLOB key.</returns>
    string GenerateBlobKey(string mimeType, string? path = default);
}
