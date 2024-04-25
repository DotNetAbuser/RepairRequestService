using Client.Enums;
using Client.Services;

namespace Client.ViewModels;

public partial class ChangePasswordVM(
    IAlertService alertService,
    INavigationService navigationService,
    IAccountManager accountManager)
    : BaseVM(alertService, navigationService)
{
    private readonly IAccountManager _accountManager = accountManager;
    
    [ObservableProperty] private string currentPassword = string.Empty;
    [ObservableProperty] private string newPassword = string.Empty;
    [ObservableProperty] private string confirmNewPassword = string.Empty;

    [RelayCommand]
    private async Task ChangePasswordAsync()
    {
        try
        {
            IsBusy = true;
            var request = new ChangePasswordRequest(
                CurrentPassword: CurrentPassword,
                NewPassword: NewPassword,
                ConfirmNewPassword: ConfirmNewPassword);
            var result = await _accountManager.ChangePasswordAsync(request);
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
            IsBusy = false;
        }
    }
}