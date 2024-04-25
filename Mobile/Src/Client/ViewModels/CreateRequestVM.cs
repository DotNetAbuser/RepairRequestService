using Client.Enums;
using Client.Services;

namespace Client.ViewModels;

public partial class CreateRequestVM(
    IAlertService alertService,
    INavigationService navigationService,
    IRequestManager requestManager,
    IEquipmentTypeManager equipmentTypeManager,
    IProblemTypeManager problemTypeManager,
    ITokenManager tokenManager) 
    : BaseVM(alertService, navigationService)
{
    private readonly IRequestManager _requestManager = requestManager;
    private readonly IEquipmentTypeManager _equipmentTypeManager = equipmentTypeManager;
    private readonly IProblemTypeManager _problemTypeManager = problemTypeManager;
    private readonly ITokenManager _tokenManager = tokenManager;
    protected override async Task AppearingView()
    {
        await base.AppearingView();
        await LoadDataAsync();
    }

    [ObservableProperty] private List<EquipmentTypeResponse> equipmentTypesList = [];
    [ObservableProperty] private List<ProblemTypeResponse> problemTypesList = [];
    
    [ObservableProperty] private EquipmentTypeResponse selectedEquipmentType;
    [ObservableProperty] private ProblemTypeResponse selectedProblemType;
    [ObservableProperty] private string problemDescription = string.Empty;
    
    [RelayCommand]
    private async Task EquipmentTypeChangedAsync()
    {
        try
        {
            IsBusy = true;
            var result = await _problemTypeManager.GetAllAsync(SelectedEquipmentType.Id.ToString());
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            SelectedProblemType = null;
            ProblemTypesList = [..result.Data];
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
    private async Task CreateRequestAsync()
    {
        try
        {
            IsBusy = true;
            var token = await _tokenManager.GetJwtAsync();
            var userId = await _tokenManager.GetUserIdAsync(token);
            var repairRequest = new RepairRequest(
                UserId: userId,
                EquipmentTypeId: SelectedEquipmentType.Id,
                ProblemTypeId: SelectedProblemType.Id,
                RepairStatusId: 1,
                ProblemDescription: ProblemDescription);
            var result = await _requestManager.CreateRequestAsync(repairRequest);
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
    
    private async Task LoadDataAsync()
    {
        try
        {
            IsBusy = true;
            var result = await _equipmentTypeManager.GetAllAsync();
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            EquipmentTypesList = [..result.Data];
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