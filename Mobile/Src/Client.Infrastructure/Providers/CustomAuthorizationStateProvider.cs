namespace Client.Infrastructure.Providers;

public class CustomAuthorizationStateProvider(ITokenManager tokenManager) : AuthenticationStateProvider
{

    private readonly ITokenManager _tokenManager = tokenManager;
    
    public async Task StateChangedAsync()
    {
        var authState = Task.FromResult(await GetAuthenticationStateAsync());
        NotifyAuthenticationStateChanged(authState);
    }
    
    public void MarkUserAsLoggedOut()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));

        NotifyAuthenticationStateChanged(authState);
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authToken = await _tokenManager.GetJwtAsync();
        if (string.IsNullOrWhiteSpace(authToken))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var userId = await _tokenManager.GetUserIdAsync(authToken);
        var role = await _tokenManager.GetUserRole(authToken);
        var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(CustomClaimTypes.UserId, userId),
            new Claim(CustomClaimTypes.Role, role)
        }, "jwt")));
        return state;

    }
    
    
    
    
    
}