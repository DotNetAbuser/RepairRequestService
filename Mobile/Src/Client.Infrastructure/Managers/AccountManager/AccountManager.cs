namespace Client.Infrastructure.Managers.AccountManager;

public class AccountManager(IHttpClientFactory factory)
    : IAccountManager
{
    private readonly IHttpClientFactory _factory = factory;
    public async Task<IResult> UpdateProfileAsync(UpdateProfileRequest request)
    {
        var response = await _factory.CreateClient("Client").PutAsJsonAsync(AccountRoutes.UpdateProfile, request);
        var result = await response.ToResult();
        return result;
    }

    public async Task<IResult> ChangePasswordAsync(ChangePasswordRequest request)
    {
        var response = await _factory.CreateClient("Client").PutAsJsonAsync(AccountRoutes.ChangePassword, request);
        var result = await response.ToResult();
        return result;
    }
}