using MediatR;
using SecretsSharing.Domain.Entities;
using SecretsSharing.Infrastructure.Abstractions;

namespace SecretsSharing.UseCases.Secrets.CreateTextSecret;

/// <summary>
/// Handler for <see cref="CreateTextSecretCommand" />.
/// </summary>
internal class CreateTextSecretCommandHandler : IRequestHandler<CreateTextSecretCommand, CreateTextSecretCommandResult>
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
    public async Task<CreateTextSecretCommandResult> Handle(CreateTextSecretCommand request, CancellationToken cancellationToken)
    {
        var newSecret = new SecretText
        {
            Content = request.TextSecretDto.Content
        };
        await dbContext.SecretTexts.AddAsync(newSecret, cancellationToken);

        var newLink = new Link
        {
            OwnerId = currentUserAccessor.GetCurrentUserId(),
            SecretId = newSecret.Id,
            SecretType = SecretType.Text,
            DeleteAfterDownload = request.TextSecretDto.DeleteAfterDownload,
        };
        await dbContext.Links.AddAsync(newLink, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTextSecretCommandResult
        {
            LinkId = newLink.Id,
        };
    }
}
