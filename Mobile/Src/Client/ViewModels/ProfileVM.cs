using Client.Enums;
using Client.Infrastructure.Managers.UserManager;
using Client.Services;
using Client.Views;

namespace Client.ViewModels;

public partial class ProfileVM(
    IAlertService alertService,
    INavigationService navigationService,
    ITokenManager tokenManager,
    IUserManager userManager)
    : BaseVM(alertService, navigationService)
{
    private readonly ITokenManager _tokenManager = tokenManager;
    private readonly IUserManager _userManager = userManager;

    protected override async Task AppearingView()
    {
        await base.AppearingView();
        IsBusy = true;
    }

    protected  override async Task DisappearingView()
    {
        await base.DisappearingView();
    }

    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string middleName = string.Empty;
    [ObservableProperty] private string email = string.Empty;

    [RelayCommand]
    private async Task RefreshDataAsync()
    {
        try
        {
            var token = await _tokenManager.GetJwtAsync();
            var userId = await _tokenManager.GetUserIdAsync(token);
            var result = await _userManager.GetByIdAsync(userId);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }

            LastName = result.Data.LastName;
            FirstName = result.Data.FirstName;
            MiddleName = result.Data.MiddleName;
            Email = result.Data.Email;
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
    private async Task GoToUpdateProfileViewAsync() =>
        await _navigationService.NavigateToAsync(nameof(UpdateProfileView), new Dictionary<string, object>
        {
            { nameof(LastName), LastName },
            { nameof(FirstName), FirstName },
            { nameof(MiddleName), MiddleName },
            { nameof(Email), Email }
        });

    [RelayCommand]
    private async Task GoToChangePasswordViewAsync() =>
        await _navigationService.NavigateToAsync(nameof(ChangePasswordView));

    [RelayCommand]
    private async Task GoToRefreshSessionsViewAsync() =>
        await _alertService.ShowAlertAsync(AlertType.Information, "В разработке");

    [RelayCommand]
    private async Task SignOutAsync()
    {
        await _tokenManager.LogoutAsync();
        await _navigationService.NavigateToAsync($"//{nameof(StartUpView)}");
    }
    
    [RelayCommand]
    private async Task GoToSupportViewAsync() =>
        await _alertService.ShowAlertAsync(AlertType.Information, "В разработке");
    
    [RelayCommand]
    private async Task GoToAboutAppViewAsync() =>
        await _alertService.ShowAlertAsync(AlertType.Information, "В разработке");

}