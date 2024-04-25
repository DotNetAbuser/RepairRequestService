namespace Core.IServices;

public interface IAccountService
{
    Task<Result> UpdateProfileAsync(UpdateProfileRequest request, string currentUserId);
    Task<Result> ChangePasswordAsync(ChangePasswordRequest request, string currentUserId);
}