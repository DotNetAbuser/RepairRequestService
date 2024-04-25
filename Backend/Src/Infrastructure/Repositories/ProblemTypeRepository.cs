namespace Infrastructure.Repositories;

public class ProblemTypeRepository(ApplicationDbContext dbContext) 
    : IProblemTypeRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<IEnumerable<ProblemTypeEntity>> GetAllByEquipmentTypeIdAsync(int equipmentTypeId)
    {
        return await _dbContext.ProblemTypes
            .AsNoTracking()
            .Where(x => x.EquipmentTypeId == equipmentTypeId)
            .ToListAsync();
    }

    public async Task<ProblemTypeEntity?> GetByNameAndEquipmentIdAsync(string problemTypeName, int equipmentTypeId)
    {
        return await _dbContext.ProblemTypes
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Name == problemTypeName && x.EquipmentTypeId == equipmentTypeId);
    }

    public async Task<ProblemTypeEntity?> GetByIdAsync(int problemTypeId)
    {
        return await _dbContext.ProblemTypes
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == problemTypeId);
    }

    public async Task UpdateAsync(ProblemTypeEntity problemTypeEntity)
    {
        _dbContext.ProblemTypes.Update(problemTypeEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateAsync(ProblemTypeEntity problemTypeEntity)
    {
        await _dbContext.ProblemTypes.AddAsync(problemTypeEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ProblemTypeEntity problemTypeEntity)
    {
        _dbContext.ProblemTypes.Remove(problemTypeEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsExistByNameAndEquipmentTypeIdAsync(string name, int equipmentTypeId)
    {
        return await _dbContext.ProblemTypes
            .AsNoTracking()
            .AnyAsync(x => x.Name == name && x.EquipmentTypeId == equipmentTypeId);
    }
}