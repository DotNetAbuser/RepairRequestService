namespace Client.Infrastructure.Managers.RequestManager;

public interface IRequestManager
{
    Task<IResult<PaginatedData<RepairResponse>>> GetPaginatedRequestsByUserid(string userId,
        int pageNumber, int pageSize,
        string? searchTerm, string? sortColumn, string? sortOrder);

    Task<IResult<PaginatedData<RepairResponse>>> GetPaginatedRequests(int pageNumber, int pageSize,
        string? searchTerm, string? sortColumn, string? sortOrder);

    Task<IResult<RepairResponse>> GetByIdAsync(string requestId);
    Task<IResult> CreateRequestAsync(RepairRequest request);
    Task<IResult> UpdateRequestAsync(string requestId, RepairRequest request);
    Task<IResult> ToggleStatusRequestAsync(string repairRequestId, RepairStatusRequest request);
}