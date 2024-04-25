using Client.Services;

namespace Client.ViewModels;

public class HomeVM(
    IAlertService alertService,
    INavigationService navigationService)
    : BaseVM(alertService, navigationService)
{
    
}