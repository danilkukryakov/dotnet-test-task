using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.UseCases.Secrets.CreateFileSecret;
using SecretsSharing.UseCases.Secrets.CreateTextSecret;
using SecretsSharing.UseCases.Secrets.DeleteSecret;
using SecretsSharing.UseCases.Secrets.GetSecretContent;
using SecretsSharing.UseCases.Secrets.GetUserSecretLinks;

namespace SecretsSharing.Web.Controllers;

/// <summary>
/// Authentication controller.
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
[Authorize]
public class SecretsController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SecretsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Get secret content.
    /// </summary>
    /// <param name="id">Link id.</param>
    /// <param name="cancellationToken">Token to monitor request cancellation.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [AllowAnonymous]
    public async Task<IActionResult> GetSecretContent(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetSecretContentCommand { SecretLinkId = id }, cancellationToken);
        return result.FileStream ?? (IActionResult)Content(result.TextContent ?? string.Empty);
    }

    /// <summary>
    /// Get secret links for current user.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor request cancellation.</param>
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<GetUserSecretLinksCommandResult> GetMySecretLinks(CancellationToken cancellationToken)
        => await mediator.Send(new GetUserSecretLinksCommand(), cancellationToken);

    /// <summary>
    /// Create new text secret.
    /// </summary>
    /// <param name="dto">DTO for text secret creation.</param>
    /// <param name="cancellationToken">Token to monitor request cancellation.</param>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<Guid> CreateTextSecret([Required] CreateTextSecretDto dto, CancellationToken cancellationToken)
        => (await mediator.Send(new CreateTextSecretCommand { TextSecretDto = dto }, cancellationToken)).LinkId;

    /// <summary>
    /// Create new file secret.
    /// </summary>
    /// <param name="file">File for secret.</param>
    /// <param name="deleteAfterDownload">Should file be deleted after download.</param>
    /// <param name="cancellationToken">Token to monitor request cancellation.</param>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<Guid> CreateFileSecret([Required] IFormFile file,
        bool deleteAfterDownload,
        CancellationToken cancellationToken)
            => (await mediator.Send(new CreateFileSecretCommand
            {
                FileSecretDto = new CreateFileSecretDto
                {
                    File = file,
                    DeleteAfterDownload = deleteAfterDownload,
                }
            }, cancellationToken)).LinkId;

    /// <summary>
    /// Remove secret link.
    /// </summary>
    /// <param name="id">Secret link id.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task Delete(Guid id, CancellationToken cancellationToken)
        => await mediator.Send(new DeleteSecretCommand { SecretLinkId = id }, cancellationToken);
}
