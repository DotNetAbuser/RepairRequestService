using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;


namespace Infrastructure.Services;

public class TokenService(IUserRepository userRepository,
    IRefreshSessionRepository refreshSessionRepository,
    IPasswordHasher passwordHasher,
    IOptions<JwtOptions> jwtOptions)
    : ITokenService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRefreshSessionRepository _refreshSessionRepository = refreshSessionRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IOptions<JwtOptions> _jwtOptions = jwtOptions;
    
    public async Task<Result<TokenResponse>> LoginAsync(TokenRequest request)
    {
        var userEntity = await _userRepository
            .GetUserByEmailWithRoleAsync(request.Email);
        if (userEntity == null)
        {
            return await Result<TokenResponse>
                .FailAsync("Пользователь с данной почтой не существует!");
        }
        var isPasswordCorrect = _passwordHasher.Verify(request.Password, userEntity.PasswordHash);
        if (!isPasswordCorrect)
        {
            return await Result<TokenResponse>
                .FailAsync("Неправильный пароль!");
        }
        if (!userEntity.IsActive)
        {
            return await Result<TokenResponse>
                .FailAsync("Пользователь имеет статус неактивен!");
        }
        var authToken = await generateJwtToken(userEntity);
        var refreshToken = generateRefreshToken();
        var refreshSessionEntity = new RefreshSessionEntity
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            Token = refreshToken,
            Expires = DateTime.UtcNow.AddDays(7),
            
            CreatedOn = DateTime.UtcNow
        };
        await _refreshSessionRepository.CreateAsync(refreshSessionEntity);
        var tokenResponse = new TokenResponse
        {
            AuthToken = authToken,
            RefreshToken = refreshToken
        };
        return await Result<TokenResponse>
            .SuccessAsync(tokenResponse, "Пользователь успешно прошёл аунтификацию.");
    }

    public async Task<Result<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var refreshSessionEntity = await _refreshSessionRepository
            .GetByRefreshTokenAsync(request.RefreshToken);
        if (refreshSessionEntity == null)
        {
            return await Result<TokenResponse>
                .FailAsync("Сессии не существует, необходимо пройти аунтификацию!");
        }
        if (refreshSessionEntity.Expires < DateTime.UtcNow)
        {
            await _refreshSessionRepository.DeleteAsync(refreshSessionEntity);
            return await Result<TokenResponse>
                .FailAsync("Сессия устарела, необходимо вновь пройти аунтификацию!");
        }
        var userEntity = await _userRepository.GetByUserIdWithRoleIncludeAsync(refreshSessionEntity.UserId);
        if (userEntity == null)
        { 
            return await Result<TokenResponse>
                .FailAsync("Пользователь не найден!");
        }
        if (!userEntity.IsActive)
        {
            return await Result<TokenResponse>
                .FailAsync("Пользователь имеет статус неактивен!");
        }
        await _refreshSessionRepository.DeleteAsync(refreshSessionEntity);
        var newAuthToken = await generateJwtToken(userEntity);
        var newRefreshToken = generateRefreshToken();
        var newRefreshSessionEntity = new RefreshSessionEntity
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            Token = newRefreshToken,
            Expires = DateTime.UtcNow.AddDays(7),
            CreatedOn = DateTime.UtcNow
        };
        await _refreshSessionRepository.CreateAsync(newRefreshSessionEntity);
        var tokenResponse = new TokenResponse
        {
            AuthToken = newAuthToken,
            RefreshToken = newRefreshToken
        };
        return await Result<TokenResponse>
            .SuccessAsync(tokenResponse, "Новый авторизационный токен успешно получен");
    }
    
    private async Task<string> generateJwtToken(UserEntity user) =>
        generateEncryptedToken(getSigningCredentials(), await getClaims(user));

    private async Task<IEnumerable<Claim>> getClaims(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new(CustomClaimTypes.UserId, user.Id.ToString()),
            new(CustomClaimTypes.Role, user.Role.Name)
        };
        return claims;
    }

    private string generateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var expiresMinutes = _jwtOptions.Value.Expires;
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
            signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        var encryptedToken = tokenHandler.WriteToken(token);

        return encryptedToken;
    }

    private string generateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private SigningCredentials getSigningCredentials()
    {
        var secretKey = _jwtOptions.Value.SecretKey;
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        return new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
    }
}