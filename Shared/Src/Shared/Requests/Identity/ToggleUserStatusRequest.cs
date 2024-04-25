namespace Shared.Requests.Identity;

public class ToggleUserStatusRequest
{
    public string UserId { get; set; } = string.Empty;
    public bool ActivateUser { get; set; }
}