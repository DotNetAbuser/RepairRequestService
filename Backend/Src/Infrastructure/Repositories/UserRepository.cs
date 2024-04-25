namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext dbContext) 
    : IUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    
    public async Task<PaginatedData<UserEntity>> GetPaginatedUsersAsync(int pageNumber, int pageSize,
        string searchTerm, string sortColumn, string sortOrder)
    {
        var query = _dbContext.Users
            .AsNoTracking();
        
        if (!string.IsNullOrWhiteSpace(searchTerm))
            query = query.Where(u =>
                u.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                u.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                u.MiddleName.ToLower().Contains(searchTerm.ToLower()) ||
                u.Email.ToLower().Contains(searchTerm.ToLower()));

        Expression<Func<UserEntity, object>> keySelector = sortColumn?.ToLower() switch
        {
            "lastname" => user => user.LastName,
            "firstname" => user => user.FirstName,
            "middlename" => user => user.MiddleName,
            "email" => user => user.Email,
            _ => user => user.CreatedOn
        };

        if (sortOrder?.ToLower() == "asc") query = query.OrderBy(keySelector);
        else query = query.OrderByDescending(keySelector);
            
        
        var data = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var totalCount = await _dbContext.Users
            .CountAsync();
        return new PaginatedData<UserEntity>(data, totalCount);
    }
    
    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task CreateAsync(UserEntity userEntity)
    {
        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<UserEntity?> GetByUserIdAsync(Guid userId)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == userId); 
    }

    public async Task<UserEntity?> GetByUserIdWithRoleIncludeAsync(Guid userId)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Id == userId); 
    }

    public async Task<UserEntity?> GetUserByEmailWithRoleAsync(string email)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Email == email); 
    }

    public async Task UpdateAsync(UserEntity userEntity)
    {
        _dbContext.Users.Update(userEntity);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<bool> IsExistForUpdateByEmailAsync(Guid userId, string email)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.Id != userId && x.Email == email);
    }
}