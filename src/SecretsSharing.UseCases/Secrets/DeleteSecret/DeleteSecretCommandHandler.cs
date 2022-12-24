using MediatR;
using SecretsSharing.Domain.Entities;
using SecretsSharing.Infrastructure.Abstractions;

namespace SecretsSharing.UseCases.Secrets.DeleteSecret;

/// <summary>
/// Handler for <see cref="DeleteSecretCommand" />.
/// </summary>
internal class DeleteSecretCommandHandler : AsyncRequestHandler<DeleteSecretCommand>
{
    private readonly IAppDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DeleteSecretCommandHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <inheritdoc />
    protected override async Task Handle(DeleteSecretCommand request, CancellationToken cancellationToken)
    {
        var link = await dbContext.Links.FindAsync(new object?[] { request.SecretLinkId, cancellationToken }, cancellationToken: cancellationToken);
        if (link == null)
        {
            throw new NullReferenceException();
        }

        if (link.SecretType == SecretType.Text)
        {
            await RemoveTextSecret(link, cancellationToken);
        }
        else if (link.SecretType == SecretType.File)
        {
            await RemoveFileSecret(link, cancellationToken);
        }

        dbContext.Links.Remove(link);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task RemoveTextSecret(Link link, CancellationToken cancellationToken)
    {
        var textSecret = await dbContext.SecretTexts.FindAsync(new object?[] { link.SecretId, cancellationToken }, cancellationToken: cancellationToken);
        if (textSecret != null)
        {
            dbContext.SecretTexts.Remove(textSecret);
        }
    }

    private async Task RemoveFileSecret(Link link, CancellationToken cancellationToken)
    {
        var fileSecret = await dbContext.SecretFiles.FindAsync(new object?[] { link.SecretId, cancellationToken }, cancellationToken: cancellationToken);
        if (fileSecret != null)
        {
            dbContext.SecretFiles.Remove(fileSecret);
        }
    }
}
