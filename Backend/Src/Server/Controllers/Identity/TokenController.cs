using Shared.Constants;

namespace Server.Controllers.Identity;

[ApiController]
[Route("api/identity/token")]
public class TokenController(ITokenService tokenService)
    : ControllerBase
{
    private readonly ITokenService _tokenService = tokenService;

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> GetTokensAsync(TokenRequest request)
    {
        var response = await _tokenService.LoginAsync(request);
        return Ok(response);
    }
    
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var response = await _tokenService.RefreshTokenAsync(request);
        return Ok(response);
    }
    
}