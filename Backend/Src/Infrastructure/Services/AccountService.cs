namespace Infrastructure.Services;

public class AccountService(IUserRepository userRepository,
    IPasswordHasher passwordHasher)
    : IAccountService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    public async Task<Result> UpdateProfileAsync(UpdateProfileRequest request, string currentUserId)
    {
        var isExistForUpdateByEmail = await _userRepository
            .IsExistForUpdateByEmailAsync(Guid.Parse(currentUserId), request.Email);
        if (isExistForUpdateByEmail)
        {
            return await Result<string>
                .FailAsync("Пользователь с данной эл. почтой уже существует!");
        }

        var user = await _userRepository
            .GetByUserIdAsync(Guid.Parse(currentUserId));
        if (user == null)
        {
            return await Result<string>
                .FailAsync("Пользователь не найден!");
        }

        user.LastName = request.LastName;
        user.FirstName = request.FirstName;
        user.MiddleName = request.MiddleName;
        user.Email = request.Email;
        user.UpdatedOn = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
        return await Result<string>
            .SuccessAsync("Профиль пользователя успешно изменена.");
    }

    public async Task<Result> ChangePasswordAsync(ChangePasswordRequest request, string currentUserId)
    {
        var user = await _userRepository
            .GetByUserIdAsync(Guid.Parse(currentUserId));
        if (user == null)
        {
            return await Result<string>
                .FailAsync("Пользователь не найден!");
        }

        var isPasswordCorrect = _passwordHasher
            .Verify(request.CurrentPassword, user.PasswordHash);
        if (!isPasswordCorrect)
        {
            return await Result<string>
                .FailAsync("Неправильный старый пароль!");
        }

        user.PasswordHash = passwordHasher.Generate(request.NewPassword);
        user.UpdatedOn = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
        return await Result<string>
            .SuccessAsync("Пароль успешно изменён.");
    }
}