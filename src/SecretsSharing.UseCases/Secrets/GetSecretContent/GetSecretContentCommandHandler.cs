using MediatR;
using SecretsSharing.Infrastructure.Abstractions;
using SecretsSharing.Domain.Entities;
using SecretsSharing.UseCases.Secrets.DeleteSecret;

namespace SecretsSharing.UseCases.Secrets.GetSecretContent;

/// <summary>
/// Handler for <see cref="GetSecretContentCommand" />.
/// </summary>
internal class GetSecretContentCommandHandler : IRequestHandler<GetSecretContentCommand, GetSecretContentCommandResult>
{
    private readonly IAppDbContext dbContext;
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetSecretContentCommandHandler(IAppDbContext dbContext, IMediator mediator)
    {
        this.dbContext = dbContext;
        this.mediator = mediator;
    }

    /// <inheritdoc />
    public async Task<GetSecretContentCommandResult> Handle(GetSecretContentCommand request, CancellationToken cancellationToken)
    {
        var link = await dbContext.Links.FindAsync(new object?[] { request.SecretLinkId, cancellationToken }, cancellationToken: cancellationToken);
        if (link == null)
        {
            throw new NullReferenceException();
        }

        var content = await GetSecretContent(link, cancellationToken);

        var result = new GetSecretContentCommandResult
        {
            LinkId = link.Id,
            Content = content,
            SecretType = link.SecretType,
        };

        if (link.DeleteAfterDownload)
        {
            await mediator.Send(new DeleteSecretCommand
            {
                SecretLinkId = link.Id
            }, CancellationToken.None);
        }

        return result;
    }

    private async Task<string> GetSecretContent(Link link, CancellationToken cancellationToken)
    {
        string? resultContent = null;

        if (link.SecretType == SecretType.Text)
        {
            resultContent = (await dbContext.SecretTexts.FindAsync(new object?[] { link.SecretId, cancellationToken }, cancellationToken: cancellationToken))?.Content;
        }
        else if (link.SecretType == SecretType.File)
        {
            resultContent = (await dbContext.SecretFiles.FindAsync(new object?[] { link.SecretId, cancellationToken }, cancellationToken: cancellationToken))?.BlobRef;
        }

        if (resultContent == null)
        {
            throw new NullReferenceException();
        }

        return resultContent;
    }
}
