using Client.Services;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class BaseVM(
    IAlertService alertService,
    INavigationService navigationService)
    : ObservableObject
{
    protected readonly IAlertService _alertService = alertService;
    protected readonly INavigationService _navigationService = navigationService;

    [ObservableProperty] protected bool isBusy;

    [RelayCommand]
    protected virtual async Task DisappearingView()
    {
        // base logic
    }
    
    [RelayCommand]
    protected virtual async Task AppearingView()
    {
        // base logic check INTERNET
    }
    

}