using MediatR;
using SecretsSharing.Infrastructure.Abstractions;
using SecretsSharing.Domain.Entities;

namespace SecretsSharing.UseCases.Secrets.GetSecretContent;

/// <summary>
/// Handler for <see cref="GetSecretContentCommand" />.
/// </summary>
internal class GetSecretContentCommandHandler : IRequestHandler<GetSecretContentCommand, GetSecretContentCommandResult>
{
    private readonly IAppDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetSecretContentCommandHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<GetSecretContentCommandResult> Handle(GetSecretContentCommand request, CancellationToken cancellationToken)
    {
        var link = await dbContext.Links.FindAsync(new object?[] { request.SecretLinkId, cancellationToken }, cancellationToken: cancellationToken);
        if (link == null)
        {
            throw new NullReferenceException();
        }

        string? resultContent = null;

        if (link.SecretType == SecretType.Text)
        {
            resultContent = (await dbContext.SecretTexts.FindAsync(new object?[] { link.SecretId, cancellationToken }, cancellationToken: cancellationToken))?.Content;
        }
        else if (link.SecretType == SecretType.File)
        {
            resultContent = (await dbContext.SecretFiles.FindAsync(new object?[] { link.SecretId, cancellationToken }, cancellationToken: cancellationToken))?.BlobRef;
        }

        if (resultContent == null) {
            throw new NullReferenceException();
        }

        // TODO: Add logic to delete file once it was read.

        return new GetSecretContentCommandResult
        {
            LinkId = link.Id,
            Content = resultContent,
            SecretType = link.SecretType,
        };
    }
}
