namespace Infrastructure.Repositories;

public class RoleRepository(ApplicationDbContext dbContext)
    : IRoleRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<IEnumerable<RoleEntity>> GetAllAsync()
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<RoleEntity?> GetByNameAsync(string name)
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Name == name);
    }
}