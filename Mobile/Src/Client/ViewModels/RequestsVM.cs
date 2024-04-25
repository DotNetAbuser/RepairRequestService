using System.Threading.Tasks;
using Client.Enums;
using Client.Services;
using Client.Views;
using CommunityToolkit.Maui.Views;
using Shared.Responses;
using Shared.Wrapper;

namespace Client.ViewModels;

public partial class RequestsVM(
    IAlertService alertService,
    INavigationService navigationService,
    IRequestManager requestManager,
    ITokenManager tokenManager,
    RequestDetailsVM requestDetailsVM)
    : BaseVM(alertService, navigationService)
{
    private readonly IRequestManager _requestManager = requestManager;
    private readonly ITokenManager _tokenManager = tokenManager;
    private readonly RequestDetailsVM _requestDetailsVM = requestDetailsVM;
    
    private int pageNumber = 1;
    private int pageSize = 10;
    private string sortColumn = "Дата создания";

    protected override async Task AppearingView()
    {
        await base.AppearingView();
        IsBusy = true;
    }

    protected override async Task DisappearingView()
    {
        await base.DisappearingView();
        RepairResponseList.Clear();
        TotalCount = 0;
    }

    public ObservableCollection<RepairResponse> RepairResponseList { get; private set; } = [];
    [ObservableProperty] private int totalCount = 0;
    
    [ObservableProperty] private string orderBy = "desc";
    [ObservableProperty] private string searchTerms = string.Empty;
    [ObservableProperty] private bool isFooterLoading;

    [ObservableProperty] private bool isAdmin;

    [RelayCommand]
    private async Task GoToCreateRequestViewAsync() =>
        await _navigationService.NavigateToAsync(nameof(CreateRequestView));

    [RelayCommand]
    private async Task SearchDataAsync() =>
        IsBusy = true;
    
    [RelayCommand]
    private async Task ChangeOrderByAsync()
    {
        OrderBy = OrderBy == "asc" ? "desc" : "asc";
        IsBusy = true;
    }
    
    [RelayCommand]
    private async Task RefreshDataAsync()
    {
        try
        {
            pageNumber = 1;
            RepairResponseList.Clear();
            var token = await _tokenManager.GetJwtAsync();
            var role = await _tokenManager.GetUserRole(token);
            IsAdmin = role.Contains("admin");
            IResult<PaginatedData<RepairResponse>> result;
            if (IsAdmin)
            {
                result = await _requestManager.GetPaginatedRequests(pageNumber, pageSize,
                    searchTerms, sortColumn, orderBy);
            }
            else
            {
                var userId = await _tokenManager.GetUserIdAsync(token);
                result = await _requestManager.GetPaginatedRequestsByUserid(userId, pageNumber, pageSize,
                    searchTerms, sortColumn, orderBy);
            }
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }
            foreach (var item in result.Data.Data)
                RepairResponseList.Add(item);
            TotalCount = result.Data.TotalCount;
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
    private async Task GetNextDataAsync()
    {
        try
        {
            IsFooterLoading = true;

            if (RepairResponseList.Count <= 0)
                return;

            if (RepairResponseList.Count >= TotalCount)
                return;

            pageNumber += 1;

            var token = await _tokenManager.GetJwtAsync();
            IResult<PaginatedData<RepairResponse>> result;
            
            if (IsAdmin)
            {
                result = await _requestManager.GetPaginatedRequests(pageNumber, pageSize,
                    searchTerms, sortColumn, orderBy);
            }
            else
            {
                var userId = await _tokenManager.GetUserIdAsync(token);
                result = await _requestManager.GetPaginatedRequestsByUserid(userId, pageNumber, pageSize,
                    searchTerms, sortColumn, orderBy);
            }
            if (!result.Succeeded)
            {
                foreach (var message in result.Messages)
                    await _alertService.ShowAlertAsync(AlertType.Error, message);
                return;
            }

            foreach (var item in result.Data.Data)
                RepairResponseList.Add(item);
            TotalCount = result.Data.TotalCount;
        }
        catch (Exception ex)
        {
            await _alertService
                .ShowAlertAsync(AlertType.Exception, ex.Message);
        }
        finally
        {
            IsFooterLoading = false;
        }
    }

    [RelayCommand]
    private async Task GoToRequestDetailsViewAsync(RepairResponse model) =>
        await _navigationService.NavigateToAsync(nameof(RequestDetailsView), 
            new Dictionary<string, object>
            {
                { "RequestId", model.Id }
            });
}