namespace Core.IRepositories;

public interface IRepairRequestRepository
{
    Task<PaginatedData<RepairRequestEntity>> GetPaginatedRequestsByUserIdAsync(Guid currentUserId, int pageNumber,
        int pageSize,
        string searchTerm, string sortColumn, string sortOrder);

    Task<RepairRequestEntity?> GetByIdWithAllIncludesAsync(Guid requestId);
    Task CreateAsync(RepairRequestEntity requestEntity);
    Task UpdateAsync(RepairRequestEntity requestRepair);

    Task<PaginatedData<RepairRequestEntity>> GetPaginatedRequestsAsync(int pageNumber, int pageSize,
        string searchTerm, string sortColumn, string sortOrder);
}