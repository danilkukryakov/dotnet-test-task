using System.ComponentModel.DataAnnotations;

namespace SecretsSharing.Domain.Entities;

/// <summary>
/// Link to secret.
/// </summary>
public class Link
{
    /// <summary>
    /// Link id.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Id of user who created the link.
    /// </summary>
    [Required]
    public int OwnerId { get; set; }

    /// <summary>
    /// Id of secret file which should be downloaded when Link is viewed.
    /// This id is connected to SecretType property because we have several tables for each SecretType.
    /// So this id will point to entity in table according to the SecretType.
    /// </summary>
    [Required]
    public Guid SecretId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Secret type.
    /// </summary>
    [Required]
    public SecretType SecretType { get; set; }

    /// <summary>
    /// Whether secret entity should be deleted after it was downloaded.
    /// </summary>
    [Required]
    public bool DeleteAfterDownload { get; set; }
}
