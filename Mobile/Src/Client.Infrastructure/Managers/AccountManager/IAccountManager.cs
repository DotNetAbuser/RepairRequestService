namespace Client.Infrastructure.Managers.AccountManager;

public interface IAccountManager
{
    Task<IResult> UpdateProfileAsync(UpdateProfileRequest request);
    Task<IResult> ChangePasswordAsync(ChangePasswordRequest request);
}