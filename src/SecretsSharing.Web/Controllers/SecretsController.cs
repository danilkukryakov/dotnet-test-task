using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.UseCases.Secrets.CreateFileSecret;
using SecretsSharing.UseCases.Secrets.CreateTextSecret;
using SecretsSharing.Domain.Entities;

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
                },
                cancellationToken)).LinkId;
}
