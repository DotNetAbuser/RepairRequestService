namespace Shared.Requests.Identity;

public class RefreshTokenRequest
{
    [Required] public string AuthToken { get; set; } = string.Empty;
    [Required] public string RefreshToken { get; set; } = string.Empty;
}