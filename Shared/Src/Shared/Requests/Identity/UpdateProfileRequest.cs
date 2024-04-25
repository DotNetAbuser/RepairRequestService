namespace Shared.Requests.Identity;

public record UpdateProfileRequest(
    [Required]
    string LastName,
    [Required]
    string FirstName,
    string MiddleName,
    [Required]
    [EmailAddress]
    string Email);