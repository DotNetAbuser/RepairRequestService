using Client.Enums;
using Client.Services;
using Client.Views;

namespace Client.ViewModels;

public partial class SignInVM(
    IAlertService alertService,
    INavigationService navigationService,
    ITokenManager tokenManager) 
    : BaseVM(alertService, navigationService)
{
    private readonly ITokenManager _tokenManager = tokenManager;
    
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string password = string.Empty;

    [RelayCommand]
    private async Task GoToSignUpViewAsync() =>
        await _navigationService.NavigateToAsync(nameof(SignUpView));

    [RelayCommand]
    private async Task SignInAsync()
    {
        try
        {
            IsBusy = true;
            var request = new TokenRequest(
                Email: Email,
                Password: Password);
            var result = await _tokenManager.LoginAsync(request);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            await _navigationService.NavigateToAsync($"//HomeGuest");
        }
        catch (Exception ex)
        {
            await _alertService.ShowAlertAsync(AlertType.Exception, ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }
}