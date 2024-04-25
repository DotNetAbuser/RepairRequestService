namespace Client.Infrastructure.Managers.RepairStatusManager;

public class RepairStatusManager(IHttpClientFactory httpClient)
    : IRepairStatusManager
{
    private readonly IHttpClientFactory _httpClient = httpClient;
    public async Task<IResult<IEnumerable<RepairStatusResponse>>> GetAllStatusesAsync()
    {
        var response = await _httpClient.CreateClient("Client").GetAsync(RepairStatusRoutes.GetAllStatuses);
        var result = await response.ToResult<IEnumerable<RepairStatusResponse>>();
        return result;
    }

    public async Task<IResult<RepairStatusResponse>> GetByIdAsync(string statusRepairRequestId)
    {
        var response = await _httpClient.CreateClient("Client").GetAsync(RepairStatusRoutes.GetById(statusRepairRequestId));
        var result = await response.ToResult<RepairStatusResponse>();
        return result;
    }

    public async Task<IResult> CreateAsync(RepairStatusRequest request)
    {
        var response = await _httpClient.CreateClient("Client").PostAsJsonAsync(RepairStatusRoutes.Create, request);
        var result = await response.ToResult();
        return result;
    }

    public async Task<IResult> UpdateStatusAsync(string statusRepairRequestId, RepairStatusRequest request)
    {
        var response = await _httpClient.CreateClient("Client").PutAsJsonAsync(RepairStatusRoutes.Update(statusRepairRequestId), request);
        var result = await response.ToResult();
        return result;
    }

    public async Task<IResult> DeleteAsync(string statusRepairRequestId)
    {
        var response = await _httpClient.CreateClient("Client").DeleteAsync(RepairStatusRoutes.Delete(statusRepairRequestId));
        var result = await response.ToResult();
        return result;
    }
}