using Client.Enums;
using Client.Infrastructure.Managers.TokenManager;
using Client.Services;
using Client.Views;

namespace Client.ViewModels;

public class StartUpVM(
    IAlertService alertService, 
    INavigationService navigationService,
    ITokenManager tokenManager) 
    : BaseVM(alertService, navigationService)
{
    private readonly ITokenManager _tokenManager = tokenManager;
    
    protected  override async Task AppearingView()
    {
        await base.AppearingView();
        await CheckAuthState();
    }

    private async Task CheckAuthState()
    {
        try
        {
            IsBusy = true;
            var token = await _tokenManager.GetJwtAsync();
            if (string.IsNullOrWhiteSpace(token))
            {
                await _navigationService.NavigateToAsync($"//{nameof(SignInView)}");
                return;
            }
            await _navigationService.NavigateToAsync("//HomeGuest");
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