namespace Shared.Requests.Identity;

public record TokenRequest(
    [Required] [EmailAddress] string Email, 
    [Required] string Password);