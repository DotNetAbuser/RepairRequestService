using Client.Enums;
using Client.Services;

namespace Client.ViewModels;

[QueryProperty(nameof(LastName), nameof(LastName))]
[QueryProperty(nameof(FirstName), nameof(FirstName))]
[QueryProperty(nameof(MiddleName), nameof(MiddleName))]
[QueryProperty(nameof(Email), nameof(Email))]
public partial class UpdateProfileVM(
    IAlertService alertService,
    INavigationService navigationService,
    ITokenManager tokenManager,
    IAccountManager accountManager)
    : BaseVM(alertService, navigationService)
{
    private readonly ITokenManager _tokenManager = tokenManager;
    private readonly IAccountManager _accountManager = accountManager;

    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string middleName = string.Empty;
    [ObservableProperty] private string email = string.Empty;

    [RelayCommand]
    private async Task UpdateProfileAsync()
    {
        try
        {
            IsBusy = true;
            var token = await _tokenManager.GetJwtAsync();
            var userId = await _tokenManager.GetUserIdAsync(token);
            var request = new UpdateProfileRequest(
                LastName: LastName,
                FirstName: FirstName,
                MiddleName: MiddleName,
                Email: Email);
            var result = await _accountManager.UpdateProfileAsync(request);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            foreach (var message in result.Messages)
                await _alertService.ShowAlertAsync(AlertType.Success, message);
            await _navigationService.NavigateBackAsync();
        }
        catch (Exception ex)
        {
            await _alertService
                .ShowAlertAsync(AlertType.Exception, ex.Message);
        }
        finally
        {
            IsBusy = true;
        }
    }
    
}