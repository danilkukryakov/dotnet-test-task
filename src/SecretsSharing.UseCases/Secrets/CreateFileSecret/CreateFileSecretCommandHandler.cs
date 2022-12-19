using MediatR;
using SecretsSharing.Domain.Entities;
using SecretsSharing.Infrastructure.Abstractions;

namespace SecretsSharing.UseCases.Secrets.CreateFileSecret;

/// <summary>
/// Handler for <see cref="CreateFileSecretCommand" />.
/// </summary>
internal class CreateTextSecretCommandHandler : IRequestHandler<CreateFileSecretCommand, CreateFileSecretCommandResult>
{
    private readonly IAppDbContext dbContext;
    private readonly ICurrentUserAccessor currentUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CreateTextSecretCommandHandler(IAppDbContext dbContext, ICurrentUserAccessor currentUserAccessor)
    {
        this.dbContext = dbContext;
        this.currentUserAccessor = currentUserAccessor;
    }

    /// <inheritdoc />
    public async Task<CreateFileSecretCommandResult> Handle(CreateFileSecretCommand request, CancellationToken cancellationToken)
    {
        // TODO: Add file upload.
        var mockBlobRef = string.Empty;

        var newSecret = new SecretFile
        {
            Name = request.FileSecretDto.File.FileName,
            MimeType = request.FileSecretDto.File.ContentType,
            BlobRef = mockBlobRef,
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
