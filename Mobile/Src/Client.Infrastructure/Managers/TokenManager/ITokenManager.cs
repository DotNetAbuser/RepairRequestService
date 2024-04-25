namespace Client.Infrastructure.Managers.TokenManager;

public interface ITokenManager
{
    Task<string> GetJwtAsync();
    Task<string> GetRefreshTokenAsync();
    Task<string> GetUserIdAsync(string token);
    Task<string> GetUserRole(string token);
    Task<IResult> LoginAsync(TokenRequest model);
    Task<IResult> RefreshAsync();
    Task<IResult> LogoutAsync();
}