namespace Infrastructure.Repositories;

public class RefreshSessionRepository(ApplicationDbContext dbContext)
    : IRefreshSessionRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<RefreshSessionEntity?> GetByRefreshTokenAsync(string token)
    {
        return await _dbContext.RefreshSessions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Token == token);
    }

    public async Task CreateAsync(RefreshSessionEntity refreshSessionEntity)
    {
        await _dbContext.RefreshSessions.AddAsync(refreshSessionEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(RefreshSessionEntity refreshSessionEntity)
    {
        _dbContext.RefreshSessions.Update(refreshSessionEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(RefreshSessionEntity refreshSessionEntity)
    {
        _dbContext.RefreshSessions.Remove(refreshSessionEntity);
        await _dbContext.SaveChangesAsync();
    }
}