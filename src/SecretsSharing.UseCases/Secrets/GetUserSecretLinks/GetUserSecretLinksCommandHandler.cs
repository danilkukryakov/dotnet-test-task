using MediatR;
using Microsoft.EntityFrameworkCore;
using SecretsSharing.Infrastructure.Abstractions;

namespace SecretsSharing.UseCases.Secrets.GetUserSecretLinks;

/// <summary>
/// Handler for <see cref="GetUserSecretLinksCommand" />.
/// </summary>
internal class GetUserSecretLinksCommandHandler : IRequestHandler<GetUserSecretLinksCommand, GetUserSecretLinksCommandResult>
{
    private readonly IAppDbContext dbContext;
    private readonly ICurrentUserAccessor currentUserAccessor;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetUserSecretLinksCommandHandler(IAppDbContext dbContext, ICurrentUserAccessor currentUserAccessor)
    {
        this.dbContext = dbContext;
        this.currentUserAccessor = currentUserAccessor;
    }

    /// <inheritdoc />
    public async Task<GetUserSecretLinksCommandResult> Handle(GetUserSecretLinksCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = currentUserAccessor.GetCurrentUserId();
        var links = await dbContext.Links.Where(link => link.OwnerId == currentUserId).ToListAsync(cancellationToken);

        var resultLinks = links.Select(link => new UserSecretLinkDto
        {
            Id = link.Id,
            SecretType = link.SecretType,
        });

        return new GetUserSecretLinksCommandResult
        {
            Links = resultLinks.ToList(),
        };
    }
}
