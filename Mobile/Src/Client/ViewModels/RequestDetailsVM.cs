using Client.Enums;
using Client.Services;
using Client.Views;

namespace Client.ViewModels;

[QueryProperty(nameof(RequestId), nameof(RequestId))]
public partial class RequestDetailsVM(
    IAlertService alertService,
    INavigationService navigationService,
    IRequestManager requestManager,
    ITokenManager tokenManager)
    : BaseVM(alertService, navigationService)
{
    private readonly IRequestManager _requestManager = requestManager;
    private readonly ITokenManager _tokenManager = tokenManager;

    private readonly string[] finishedStatuses = ["Отменена", "Выполнена"];

    protected override async Task AppearingView()
    {
        await base.AppearingView();
        IsBusy = true;
    }
    
    [ObservableProperty] private string requestId = string.Empty;
    [ObservableProperty] private string equipmentType = string.Empty;
    [ObservableProperty] private string problemType = string.Empty;
    [ObservableProperty] private string repairStatus = string.Empty;
    [ObservableProperty] private string problemDescription = string.Empty;
    [ObservableProperty] private string lastName = string.Empty;
    [ObservableProperty] private string firstName = string.Empty;
    [ObservableProperty] private string middleName = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private DateTime created;

    [ObservableProperty] private bool isAdmin;
    [ObservableProperty] private bool isEditable;
    
    [RelayCommand]
    private async Task RefreshDataAsync()
    {
        try
        {
            IsBusy = true;
            var result = await _requestManager.GetByIdAsync(RequestId);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }

            EquipmentType = result.Data.EquipmentType;
            ProblemType = result.Data.ProblemType;
            RepairStatus = result.Data.RepairStatus;
            ProblemDescription = result.Data.ProblemDescription;
            LastName = result.Data.LastName;
            FirstName = result.Data.FirstName;
            MiddleName = result.Data.MiddleName;
            Email = result.Data.Email;
            Created = result.Data.Created;

            var token = await _tokenManager.GetJwtAsync();
            var role = await _tokenManager.GetUserRole(token);
            IsAdmin = role.Contains("admin");
            IsEditable = !finishedStatuses.Contains(RepairStatus);
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
    private async Task GoToEditRequestViewAsync() =>
        await _navigationService.NavigateToAsync(nameof(EditRequestView),
            new Dictionary<string, object>
            {
                { "RequestId", RequestId },
                { "EquipmentType", EquipmentType },
                { "ProblemType", ProblemType },
                { "ProblemDescription", ProblemDescription },
                { "RepairStatus", RepairStatus }
            });
    
    
    [RelayCommand]
    private async Task CancelRequestAsync()
    {
        try
        {
            var isConfirmed = await _alertService.ShowConfirmationAsync(AlertType.Question, "Вы уверены что хотите отменить заявку?");
            if (!isConfirmed)
                return;
            
            var canceledRepairStatus = new RepairStatusRequest
            {
                Id = 4,
                Name = "Отменена"
            };
            var result = await _requestManager.ToggleStatusRequestAsync(RequestId, canceledRepairStatus);
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            foreach (var message in result.Messages)
                await _alertService.ShowAlertAsync(AlertType.Success, message);
            IsBusy = true;
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