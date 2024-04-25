namespace Infrastructure.Repositories;

public class EquipmentTypeRepository(ApplicationDbContext dbContext)
    : IEquipmentTypeRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<EquipmentTypeEntity?> GetByNameAsync(string name)
    {
        return await _dbContext.EquipmentTypes
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Name == name);
    }
    
    public async Task<EquipmentTypeEntity?> GetByIdAsync(int equipmentTypeId)
    {
        return await _dbContext.EquipmentTypes
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == equipmentTypeId);
    }

    public async Task<IEnumerable<EquipmentTypeEntity>> GetAllAsync()
    {
        return await _dbContext.EquipmentTypes
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task CreateAsync(EquipmentTypeEntity equipmentTypeEntity)
    {
        await _dbContext.EquipmentTypes.AddAsync(equipmentTypeEntity);
        await _dbContext.SaveChangesAsync();
    }
    

    public async Task UpdateAsync(EquipmentTypeEntity equipmentTypeEntity)
    {
        _dbContext.EquipmentTypes.Update(equipmentTypeEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(EquipmentTypeEntity equipmentTypeEntity)
    {
        _dbContext.EquipmentTypes.Remove(equipmentTypeEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsExistByNameAsync(string name)
    {
        return await _dbContext.EquipmentTypes
            .AsNoTracking()
            .AnyAsync(x => x.Name == name);
    }
    
    public async Task<bool> IsExistForUpdateByNameAndEquipmentTypeIdAsync(string name, int equipmentTypeId)
    {
        return await _dbContext.EquipmentTypes
            .AsNoTracking()
            .AnyAsync(x => x.Id != equipmentTypeId && x.Name == name);
    }
}