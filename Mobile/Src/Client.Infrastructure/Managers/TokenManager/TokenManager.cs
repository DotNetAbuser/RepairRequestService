using System.Reflection;
using System.Text;

namespace Client.Infrastructure.Managers.TokenManager;

public class TokenManager(IHttpClientFactory factory,
    IStorageService storageService)
    : ITokenManager
{
    private readonly IHttpClientFactory _factory = factory;
    private readonly IStorageService _storageService = storageService;

    public async Task<IResult> LoginAsync(TokenRequest tokenRequest)
    {
        var response = await _factory.CreateClient("Client").PostAsJsonAsync(TokenRoutes.GetTokens, tokenRequest);
        var result = await response.ToResult<TokenResponse>();
        if (!result.Succeeded)
        {
            return await Result.FailAsync(result.Messages);
        }
        await _storageService.SetItemAsStringAsync(Storage.Local.AuthToken, result.Data.AuthToken);
        await _storageService.SetItemAsStringAsync(Storage.Local.RefreshToken, result.Data.RefreshToken);
        return await Result.SuccessAsync();
    }
    public async Task<IResult> LogoutAsync()
    {
        await _storageService.RemoveItemAsync(Storage.Local.AuthToken);
        await _storageService.RemoveItemAsync(Storage.Local.RefreshToken);
        return await Result.SuccessAsync("Пользователь успешно вышел.");
    }
    
    public async Task<IResult> RefreshAsync()
    {
        var authToken = await GetJwtAsync();
        var refreshToken = await GetRefreshTokenAsync();
        var refreshTokenRequest = new RefreshTokenRequest
        {
            AuthToken = authToken,
            RefreshToken = refreshToken
        };
        var response = await _factory
            .CreateClient("Client").PostAsJsonAsync(TokenRoutes.RefreshToken, refreshTokenRequest);
        var result = await response.ToResult<TokenResponse>();
        if (!result.Succeeded)
            return await Result.FailAsync();
        await _storageService
            .SetItemAsStringAsync(Storage.Local.AuthToken, result.Data.AuthToken);
        await _storageService
            .SetItemAsStringAsync(Storage.Local.RefreshToken, result.Data.RefreshToken);
        return await Result.SuccessAsync();
    }

    public async Task<string> GetUserIdAsync(string token)
    {
        var authToken = new JwtSecurityToken(token);
        return authToken.Claims.First(c => c.Type == CustomClaimTypes.UserId).Value;
    }
    
    
    public async Task<string> GetUserRole(string token)
    {
        var authToken = new JwtSecurityToken(token);
        return authToken.Claims.First(c => c.Type == CustomClaimTypes.Role).Value;
    }

    public async Task<string> GetJwtAsync() =>
        await _storageService.GetItemAsStringAsync(Storage.Local.AuthToken);

    public async Task<string> GetRefreshTokenAsync() =>
        await _storageService.GetItemAsStringAsync(Storage.Local.RefreshToken);
}