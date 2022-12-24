namespace SecretsSharing.UseCases.Secrets.GetUserSecretLinks;

/// <summary>
/// Get user secret links.
/// </summary>
public class GetUserSecretLinksCommandResult
{
    /// <summary>
    /// Links.
    /// </summary>
    public required List<UserSecretLinkDto> Links { get; init; }
}
