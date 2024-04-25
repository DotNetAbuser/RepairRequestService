namespace Core.IServices;

public interface IRepairStatusService
{
    Task<Result<IEnumerable<RepairStatusResponse>>> GetAllStatusesAsync();
    Task<Result<RepairStatusResponse>> GetByIdAsync(string statusRepairRequestId);
    Task<Result> CreateAsync(RepairStatusRequest request);
    Task<Result> UpdateStatusAsync(string statusRepairRequestId, RepairStatusRequest request);
    Task<Result> DeleteAsync(string statusRepairRequestId);
}