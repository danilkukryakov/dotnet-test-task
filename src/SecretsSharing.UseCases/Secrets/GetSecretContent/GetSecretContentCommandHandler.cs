using MediatR;
using SecretsSharing.Infrastructure.Abstractions;
using SecretsSharing.Domain.Entities;
using SecretsSharing.UseCases.Secrets.DeleteSecret;
using Microsoft.AspNetCore.Mvc;

namespace SecretsSharing.UseCases.Secrets.GetSecretContent;

/// <summary>
/// Handler for <see cref="GetSecretContentCommand" />.
/// </summary>
internal class GetSecretContentCommandHandler : IRequestHandler<GetSecretContentCommand, GetSecretContentCommandResult>
{
    private readonly IAppDbContext dbContext;
    private readonly IMediator mediator;
    private readonly IBlobStorage blobStorage;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetSecretContentCommandHandler(IAppDbContext dbContext, IMediator mediator,
        IBlobStorage blobStorage)
    {
        this.dbContext = dbContext;
        this.mediator = mediator;
        this.blobStorage = blobStorage;
    }

    /// <inheritdoc />
    public async Task<GetSecretContentCommandResult> Handle(GetSecretContentCommand request, CancellationToken cancellationToken)
    {
        var link = await dbContext.Links.FindAsync(new object?[] { request.SecretLinkId, cancellationToken }, cancellationToken: cancellationToken);
        if (link == null)
        {
            throw new NullReferenceException();
        }

        var result = new GetSecretContentCommandResult
        {
            LinkId = link.Id,
            SecretType = link.SecretType,
        };

        if (link.SecretType == SecretType.Text)
        {
            result.TextContent = await GetTextSecretContent(link, cancellationToken);
        }
        else if (link.SecretType == SecretType.File)
        {
            result.FileStream = await GetFileSecretContent(link, cancellationToken);
        }

        if (link.DeleteAfterDownload)
        {
            await mediator.Send(new DeleteSecretCommand
            {
                SecretLinkId = link.Id
            }, CancellationToken.None);
        }

        return result;
    }

    private async Task<string> GetTextSecretContent(Link link, CancellationToken cancellationToken)
    {
        if (link.SecretType == SecretType.Text)
        {
            return (
                await dbContext.SecretTexts.FindAsync(
                    new object?[] { link.SecretId, cancellationToken },
                    cancellationToken: cancellationToken)
                )?.Content ?? string.Empty;
        }

        return string.Empty;
    }

    private async Task<FileStreamResult> GetFileSecretContent(Link link, CancellationToken cancellationToken)
    {
        var file = await dbContext.SecretFiles.FindAsync(new object?[] { link.SecretId, cancellationToken }, cancellationToken: cancellationToken);

        if (file == null)
        {
            throw new Exception("File not found.");
        }

        var fileStream = await blobStorage.GetAsync(file.BlobRef, cancellationToken);
        return new FileStreamResult(fileStream, file.MimeType)
        {
            FileDownloadName = file.Name,
        };
    }
}
