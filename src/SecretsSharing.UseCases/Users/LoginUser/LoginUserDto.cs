using System.ComponentModel.DataAnnotations;

namespace SecretsSharing.UseCases.Users.LoginUser;

/// <summary>
/// DTO for login use case.
/// </summary>
public class LoginUserDto
{
    /// <summary>
    /// Email.
    /// </summary>
    [EmailAddress]
    [Required]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; init; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; init; }
}
