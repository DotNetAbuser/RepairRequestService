namespace Core.IServices;

public interface ITokenService
{
    Task<Result<TokenResponse>> LoginAsync(TokenRequest request);
    Task<Result<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}