namespace SecretsSharing.Infrastructure.Abstractions;

/// <summary>
/// Blob storage service.
/// </summary>
public interface IBlobStorage
{
    /// <summary>
    /// Get bucket name. The section.
    /// </summary>
    string Bucket { get; }

    /// <summary>
    /// Download BLOB from the storage.
    /// </summary>
    /// <param name="key">BLOB key.</param>
    /// <param name="cancellationToken">Cancellation token to monitor request cancellation.</param>
    /// <returns>Content stream.</returns>
    Task<Stream> GetAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove BLOB from the storage.
    /// </summary>
    /// <param name="key">BLOB key.</param>
    /// <param name="cancellationToken">Cancellation token to monitor request cancellation.</param>
    /// <returns>Awaitable task.</returns>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Upload BLOB to the storage. If file exists it will be overwritten.
    /// </summary>
    /// <param name="key">BLOB key.</param>
    /// <param name="stream">Content stream.</param>
    /// <param name="publicReadable">Determines whether an anonymous user can read a BLOB from storage or not.</param>
    /// <param name="cancellationToken">Cancellation token to monitor request cancellation.</param>
    /// <returns>Awaitable task.</returns>
    Task PostAsync(string key, Stream stream, bool publicReadable, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generate BLOB key.
    /// </summary>
    /// <param name="mimeType">MIME type.</param>
    /// <param name="customPath">Custom path in key.</param>
    /// <returns>BLOB key.</returns>
    public string GenerateBlobKey(string mimeType, string? customPath = default);

    /// <summary>
    /// Check if the BLOB exists.
    /// </summary>
    /// <param name="key">BLOB key.</param>
    /// <param name="cancellationToken">Cancellation token to monitor request cancellation.</param>
    /// <returns><c>True</c> if exists, <c>false</c> otherwise.</returns>
    Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// List BLOB's.
    /// </summary>
    /// <param name="pattern">Search pattern.</param>
    /// <param name="cancellationToken">Cancellation token to monitor request cancellation.</param>
    /// <returns>List of BLOB keys.</returns>
    Task<IEnumerable<string>> ListAsync(string pattern, CancellationToken cancellationToken = default);
}
