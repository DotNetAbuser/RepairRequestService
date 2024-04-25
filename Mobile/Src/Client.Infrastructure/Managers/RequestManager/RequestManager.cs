namespace Client.Infrastructure.Managers.RequestManager;

public class RequestManager(IHttpClientFactory factory) 
    : IRequestManager
{
    private readonly IHttpClientFactory _factory = factory;
    public async Task<IResult<PaginatedData<RepairResponse>>> GetPaginatedRequestsByUserid(string userId,
        int pageNumber, int pageSize, string? searchTerm, string? sortColumn, string? sortOrder)
    {
        var response = await _factory.CreateClient("Client").GetAsync(RequestRoutes.GetPaginatedAllRequestsByUserId(userId,
            pageNumber, pageSize, searchTerm, sortColumn, sortOrder));
        var result = await response.ToResult<PaginatedData<RepairResponse>>();
        return result;
    }

    public async Task<IResult<PaginatedData<RepairResponse>>> GetPaginatedRequests(int pageNumber, int pageSize, string? searchTerm, string? sortColumn, string? sortOrder)
    {
        var response = await _factory.CreateClient("Client").GetAsync(RequestRoutes.GetPaginatedAll(pageNumber, pageSize,
            searchTerm, sortColumn, sortOrder));
        var result = await response.ToResult<PaginatedData<RepairResponse>>();
        return result;
    }

    public async Task<IResult<RepairResponse>> GetByIdAsync(string requestId)
    {
        var response = await _factory.CreateClient("Client").GetAsync(RequestRoutes.GetById(requestId));
        var result = await response.ToResult<RepairResponse>();
        return result;
    }

    public async Task<IResult> CreateRequestAsync(RepairRequest request)
    {
        var response = await _factory.CreateClient("Client").PostAsJsonAsync(RequestRoutes.Create, request);
        var result = await response.ToResult();
        return result;
    }

    public async Task<IResult> UpdateRequestAsync(string requestId, RepairRequest request)
    {
        var response = await _factory.CreateClient("Client").PutAsJsonAsync(RequestRoutes.Update(requestId), request);
        var result = await response.ToResult();
        return result;
    }

    public async Task<IResult> ToggleStatusRequestAsync(string repairRequestId, RepairStatusRequest request)
    {
        var response = await _factory.CreateClient("Client").PutAsJsonAsync(RequestRoutes.ToggleStatusRequest(repairRequestId), request);
        var result = await response.ToResult();
        return result;
    }
}