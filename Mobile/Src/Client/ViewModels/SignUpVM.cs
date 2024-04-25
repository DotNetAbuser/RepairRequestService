using Client.Enums;
using Client.Infrastructure.Managers.UserManager;
using Client.Services;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class SignUpVM(
    IAlertService alertService,
    INavigationService navigationService,
    IUserManager userManager)
    : BaseVM(alertService, navigationService)
{
    private readonly IUserManager _userManager = userManager;
    
    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string middleName = string.Empty;

    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string password = string.Empty;
    [ObservableProperty] private bool agreeTerms;


    [RelayCommand]
    private async Task ChangeAgreeTerms() =>
        AgreeTerms = !AgreeTerms;
    
    [RelayCommand]
    private async Task SignUpAsync()
    {
        try
        {
            IsBusy = true;
            var request = new RegisterRequest(
                LastName: LastName,
                FirstName: FirstName,
                MiddleName: MiddleName,
                Email: Email,
                Password: Password,
                AgreeTerms: AgreeTerms,
                ActivateUser: true);
            var result = await _userManager.RegisterUserAsync(request);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
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

    [RelayCommand]
    private async Task GoToSignInViewAsync() =>
        await _navigationService.NavigateBackAsync();

}