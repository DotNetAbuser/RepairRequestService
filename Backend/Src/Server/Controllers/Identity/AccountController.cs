namespace Server.Controllers.Identity;

[ApiController]
[Route("api/identity/account")]
public class AccountController(IAccountService accountService)
    : ControllerBase
{
    private readonly IAccountService _accountService = accountService;
    
    [HttpPut("update-profile")]
    [Authorize]
    public async Task<ActionResult> UpdateProfileAsync(UpdateProfileRequest request)
    {
        var currentUserId = HttpContext.User.GetLoggedInUserId<string>();
        return Ok(await _accountService.UpdateProfileAsync(request, currentUserId));
    }
    
    [HttpPut("change-password")]
    [Authorize]
    public async Task<ActionResult> ChangePasswordAsync(ChangePasswordRequest request)
    {
        var currentUserId = HttpContext.User.GetLoggedInUserId<string>();
        return Ok(await _accountService.ChangePasswordAsync(request, currentUserId));
    }

}