using MediatR;
using SecretsSharing.Domain.Entities;
using SecretsSharing.Domain.Exceptions;
using SecretsSharing.Infrastructure.Abstractions;

namespace SecretsSharing.UseCases.Secrets.CreateFileSecret;

/// <summary>
/// Handler for <see cref="CreateFileSecretCommand" />.
/// </summary>
internal class CreateFileSecretCommandHandler : IRequestHandler<CreateFileSecretCommand, CreateFileSecretCommandResult>
{
    private readonly IAppDbContext dbContext;
    private readonly ICurrentUserAccessor currentUserAccessor;
    private readonly IBlobStorage blobStorage;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CreateFileSecretCommandHandler(IAppDbContext dbContext, ICurrentUserAccessor currentUserAccessor,
        IBlobStorage blobStorage)
    {
        this.dbContext = dbContext;
        this.currentUserAccessor = currentUserAccessor;
        this.blobStorage = blobStorage;
    }

    /// <inheritdoc />
    public async Task<CreateFileSecretCommandResult> Handle(CreateFileSecretCommand request, CancellationToken cancellationToken)
    {
        var mimeType = request.FileSecretDto.File.ContentType;
        var blobRef = blobStorage.GenerateBlobKey(mimeType);

        try
        {
            await blobStorage.PostAsync(blobRef, request.FileSecretDto.File.OpenReadStream(), true, cancellationToken);
        }
        catch
        {
            throw new DomainException("Can't upload file.");
        }

        var newSecret = new SecretFile
        {
            Name = request.FileSecretDto.File.FileName,
            MimeType = mimeType,
            BlobRef = blobRef,
        };
        await dbContext.SecretFiles.AddAsync(newSecret, cancellationToken);

        var newLink = new Link
        {
            OwnerId = currentUserAccessor.GetCurrentUserId(),
            SecretId = newSecret.Id,
            SecretType = SecretType.File,
            DeleteAfterDownload = request.FileSecretDto.DeleteAfterDownload,
        };
        await dbContext.Links.AddAsync(newLink, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateFileSecretCommandResult
        {
            LinkId = newLink.Id,
        };
    }
}
