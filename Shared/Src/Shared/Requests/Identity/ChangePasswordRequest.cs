namespace Shared.Requests.Identity;

public record ChangePasswordRequest(
    [Required]
    [MinLength(6)]
    [MaxLength(20)]
    string CurrentPassword,
    [Required]
    [MinLength(6)]
    [MaxLength(20)]
    string NewPassword,
    [Required]
    [MinLength(6)]
    [MaxLength(20)]
    string ConfirmNewPassword);