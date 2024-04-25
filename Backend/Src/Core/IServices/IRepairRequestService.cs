namespace Core.IServices;

public interface IRepairRequestService
{
    Task<Result<PaginatedData<RepairResponse>>> GetPaginatedRequestsByUserid(string currentUserId, string userId,
        int pageNumber, int pageSize,
        string searchTerm, string sortColumn, string sortOrder);

    Task<Result<PaginatedData<RepairResponse>>> GetPaginatedRequests(int pageNumber, int pageSize,
        string searchTerm, string sortColumn, string sortOrder);

    Task<Result<RepairResponse>> GetById(string currentUserId, string requestId);
    Task<Result> CreateRequestAsync(string currentUserId, RepairRequest request);
    Task<Result> UpdateRequestAsync(string currentUserId, string requestId, RepairRequest request);
    Task<Result> ToggleRepairStatusAsync(string currentUserId, string repairRequestId, RepairStatusRequest request);
}