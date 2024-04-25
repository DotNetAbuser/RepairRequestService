namespace Infrastructure.Repositories;

public class RepairStatusRepository(ApplicationDbContext dbContext)
    : IRepairStatusRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<RepairStatusEntity?> GetByNameAsync(string repairStatusName)
    {
        return await _dbContext.RepairStatuses
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Name == repairStatusName);
    }

    public async Task<IEnumerable<RepairStatusEntity>> GetAllAsync()
    {
        return await _dbContext.RepairStatuses
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<RepairStatusEntity?> GetByIdAsync(int statusId)
    {
        return await _dbContext.RepairStatuses
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == statusId);
    }

    public async Task UpdateAsync(RepairStatusEntity repairStatusEnitity)
    {
        _dbContext.RepairStatuses.Update(repairStatusEnitity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateAsync(RepairStatusEntity repairStatusEnitity)
    {
        await _dbContext.RepairStatuses.AddAsync(repairStatusEnitity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(RepairStatusEntity statusRepairRequestEntity)
    {
        _dbContext.RepairStatuses.Remove(statusRepairRequestEntity);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<bool> IsExistForUpdateByName(int statusRepairRequestId, string name)
    {
        return await _dbContext.RepairStatuses
            .AsNoTracking()
            .AnyAsync(x => x.Id != statusRepairRequestId && x.Name == name);
    }

    public async Task<bool> IsExistByName(string name)
    {
        return await _dbContext.RepairStatuses
            .AsNoTracking()
            .AnyAsync(x => x.Name == name);
    }
}