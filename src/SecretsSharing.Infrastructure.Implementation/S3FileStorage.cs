using System.Net;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using SecretsSharing.Infrastructure.Abstractions;

namespace SecretsSharing.Infrastructure.Implementation;

/// <summary>
/// S3 file storage.
/// </summary>
public class S3FileStorage : IBlobStorage, IDisposable
{
    private readonly AmazonS3Client s3Client;
    private readonly IKeyGenerationStrategy keyGenerationStrategy;

    /// <inheritdoc />
    public string Bucket { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="settings">S3 Settings.</param>
    /// <param name="keyGenerationStrategy">Blob key generation strategy.</param>
    public S3FileStorage(S3Settings settings, IKeyGenerationStrategy keyGenerationStrategy)
    {
        this.keyGenerationStrategy = keyGenerationStrategy;
        Bucket = settings.BucketName;

        var config = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(settings.RegionName),
            ForcePathStyle = settings.ForcePathStyle,
        };

        if (!string.IsNullOrEmpty(settings.ServiceUrl))
        {
            config.ServiceURL = settings.ServiceUrl;
        }
        else
        {
            throw new Exception("Service URL was not provided.");
        }

        if (!string.IsNullOrEmpty(settings.AccessKey))
        {
            var credentials = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
            s3Client = new AmazonS3Client(credentials, config);
        }
        else
        {
            throw new Exception("AccessKey and SecretKey were not provided.");
        }
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            var objectMetadata = await s3Client.GetObjectMetadataAsync(Bucket, key, cancellationToken);
            return true;
        }
        catch (AmazonS3Exception ex)
        {
            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }

            throw new Exception("Cannot get object metadata.", ex);
        }
    }

    /// <inheritdoc />
    public async Task<Stream> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        var response = await s3Client.GetObjectAsync(Bucket, key, cancellationToken);
        return response.ResponseStream;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<string>> ListAsync(string pattern, CancellationToken cancellationToken = default)
    {
        await CreateBucketIfNotExistsAsync();
        var listObjectsResponse = await s3Client.ListObjectsV2Async(new ListObjectsV2Request
        {
            BucketName = Bucket,
            Prefix = pattern,
        }, cancellationToken);

        return listObjectsResponse.S3Objects.Select(s => s.Key).ToList();
    }

    /// <inheritdoc />
    public async Task PostAsync(string key, Stream stream, bool publicReadable, CancellationToken cancellationToken = default)
    {
        try
        {
            await CreateBucketIfNotExistsAsync();
            using var transferUtility = new TransferUtility(s3Client);
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                CannedACL = publicReadable ? S3CannedACL.PublicRead : S3CannedACL.Private,
                InputStream = stream,
                BucketName = Bucket,
                Key = key,
            };
            await transferUtility.UploadAsync(uploadRequest, cancellationToken);
        }
        catch (HttpRequestException exception)
        {
            throw new Exception("External data storage is not available.", exception);
        }
    }

    /// <inheritdoc />
    public string GenerateBlobKey(string mimeType, string? customPath = default)
    {
        return keyGenerationStrategy.GenerateBlobKey(mimeType, customPath);
    }

    /// <inheritdoc />
    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        if (await ExistsAsync(key, cancellationToken))
        {
            await s3Client.DeleteObjectAsync(Bucket, key, cancellationToken);
        }
    }

    private async Task CreateBucketIfNotExistsAsync()
    {
        if (!await AmazonS3Util.DoesS3BucketExistV2Async(s3Client, Bucket))
        {
            var putBucketRequest = new PutBucketRequest
            {
                BucketName = Bucket,
                UseClientRegion = true
            };

            await s3Client.PutBucketAsync(putBucketRequest);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose patter implementation.
    /// </summary>
    /// <param name="disposing">Dispose managed resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            s3Client.Dispose();
        }
    }
}
