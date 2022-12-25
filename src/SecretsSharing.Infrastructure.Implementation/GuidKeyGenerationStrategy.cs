using MimeTypes;
using SecretsSharing.Infrastructure.Abstractions;

namespace SecretsSharing.Infrastructure.Implementation;

/// <summary>
/// Strategy to generate key based on GUID. For example "3c1a97f0b649bfb073525e4d0a9533.json".
/// </summary>
public class GuidKeyGenerationStrategy : IKeyGenerationStrategy
{
    /// <inheritdoc />
    public string GenerateBlobKey(string mimeType, string? path = default)
    {
        var fileName = Guid.NewGuid().ToString("N");
        var fileKey = fileName[2..] + MimeTypeMap.GetExtension(mimeType, false);

        if (string.IsNullOrEmpty(path))
        {
            return fileKey;
        }
        return $"{Path.TrimEndingDirectorySeparator(path)}/{fileKey}";
    }
}
