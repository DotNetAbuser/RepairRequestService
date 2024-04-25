namespace Client.Infrastructure.Managers.RepairStatusManager;

public interface IRepairStatusManager
{
    Task<IResult<IEnumerable<RepairStatusResponse>>> GetAllStatusesAsync();
    Task<IResult<RepairStatusResponse>> GetByIdAsync(string statusRepairRequestId);
    Task<IResult> CreateAsync(RepairStatusRequest request);
    Task<IResult> UpdateStatusAsync(string statusRepairRequestId, RepairStatusRequest request);
    Task<IResult> DeleteAsync(string statusRepairRequestId);
}