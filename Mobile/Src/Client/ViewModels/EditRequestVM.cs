using Client.Enums;
using Client.Services;

namespace Client.ViewModels;

[QueryProperty(nameof(RequestId), nameof(RequestId))]
[QueryProperty(nameof(EquipmentType), nameof(EquipmentType))]
[QueryProperty(nameof(ProblemType), nameof(ProblemType))]
[QueryProperty(nameof(ProblemDescription), nameof(ProblemDescription))]
[QueryProperty(nameof(RepairStatus), nameof(RepairStatus))]
public partial class EditRequestVM(
    IAlertService alertService,
    INavigationService navigationService,
    IEquipmentTypeManager equipmentTypeManager,
    IProblemTypeManager problemTypeManager,
    IRepairStatusManager repairStatusManager,
    IRequestManager requestManager,
    ITokenManager tokenManager)
    : BaseVM(alertService, navigationService)
{
    private readonly IEquipmentTypeManager _equipmentTypeManager = equipmentTypeManager;
    private readonly IProblemTypeManager _problemTypeManager = problemTypeManager;
    private readonly IRepairStatusManager _repairStatusManager = repairStatusManager;
    private readonly IRequestManager _requestManager = requestManager;
    private readonly ITokenManager _tokenManager = tokenManager;

    protected override async Task AppearingView()
    {
        await base.AppearingView();
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            IsBusy = true;
            var result1 = await _equipmentTypeManager.GetAllAsync();
            if (!result1.Succeeded)
            {
                foreach (var message in result1.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            EquipmentTypesList = [..result1.Data];
            SelectedEquipmentType = EquipmentTypesList.SingleOrDefault(x => x.Name == EquipmentType);
            
            var result2 = await problemTypeManager.GetAllAsync(SelectedEquipmentType.Id.ToString());
            if (!result2.Succeeded)
            {
                foreach (var message in result2.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            ProblemTypesList = [..result2.Data];
            SelectedProblemType = ProblemTypesList.SingleOrDefault(x => x.Name == ProblemType);

            var result3 = await _repairStatusManager.GetAllStatusesAsync();
            if (!result3.Succeeded)
            {
                foreach (var message in result3.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            RepairStatusesList = [..result3.Data];
            SelectedRepairStatus = RepairStatusesList.SingleOrDefault(x => x.Name == RepairStatus);
            
            var token = await _tokenManager.GetJwtAsync();
            var role = await _tokenManager.GetUserRole(token);
            IsAdmin = role.Contains("admin");
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
    
    [ObservableProperty] private IEnumerable<EquipmentTypeResponse> equipmentTypesList = [];
    [ObservableProperty] private IEnumerable<ProblemTypeResponse> problemTypesList = [];
    
    [ObservableProperty] private IEnumerable<RepairStatusResponse> repairStatusesList = [];

    [ObservableProperty] private EquipmentTypeResponse selectedEquipmentType;
    [ObservableProperty] private ProblemTypeResponse selectedProblemType;

    [ObservableProperty] private RepairStatusResponse selectedRepairStatus;
    
    [ObservableProperty] private string requestId = string.Empty;
    [ObservableProperty] private string equipmentType = string.Empty;
    [ObservableProperty] private string problemType = string.Empty;
    [ObservableProperty] private string problemDescription = string.Empty;
    [ObservableProperty] private string repairStatus = string.Empty;

    [ObservableProperty] private bool isAdmin;
    
    [RelayCommand]
    private async Task EquipmentTypeChangedAsync()
    {
        try
        {
            if (IsBusy)
                return;
            
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
    private async Task UpdateRequestAsync()
    {
        try
        {
            IsBusy = true;
            var token = await _tokenManager.GetJwtAsync();
            var userId = await _tokenManager.GetUserIdAsync(token);
            var repairRequest = new RepairRequest(
                UserId: userId,
                EquipmentTypeId: SelectedEquipmentType.Id,
                ProblemTypeId: SelectedEquipmentType.Id,
                RepairStatusId: SelectedRepairStatus.Id,
                ProblemDescription: ProblemDescription);
            var result = await _requestManager.UpdateRequestAsync(RequestId, repairRequest);
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