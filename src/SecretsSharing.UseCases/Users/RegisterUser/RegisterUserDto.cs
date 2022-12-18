using System.ComponentModel.DataAnnotations;

namespace SecretsSharing.UseCases.Users.RegisterUser;

/// <summary>
/// Register User DTO.
/// </summary>
public class RegisterUserDto
{
    /// <summary>
    /// Email.
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(256, ErrorMessage = "The {0} must be less than {1} characters long.")]
    public required string Email { get; init; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public required string Password { get; init; }
}
