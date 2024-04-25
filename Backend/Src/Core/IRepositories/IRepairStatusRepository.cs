namespace Core.IRepositories;

public interface IRepairStatusRepository
{
    Task<RepairStatusEntity?> GetByNameAsync(string repairStatusName);
    Task<IEnumerable<RepairStatusEntity>> GetAllAsync();
    Task<RepairStatusEntity?> GetByIdAsync(int statusId);
    Task UpdateAsync(RepairStatusEntity repairStatusEnitity);
    Task CreateAsync(RepairStatusEntity repairStatusEnitity);
    Task DeleteAsync(RepairStatusEntity statusRepairRequestEntity);
    Task<bool> IsExistForUpdateByName(int statusRepairRequestId, string name);
    Task<bool> IsExistByName(string name);
}