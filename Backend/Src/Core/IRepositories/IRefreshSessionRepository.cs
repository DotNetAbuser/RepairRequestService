namespace Core.IRepositories;

public interface IRefreshSessionRepository
{
    Task<RefreshSessionEntity?> GetByRefreshTokenAsync(string token);
    Task CreateAsync(RefreshSessionEntity refreshSessionEntity);
    Task UpdateAsync(RefreshSessionEntity refreshSessionEntity);
    Task DeleteAsync(RefreshSessionEntity refreshSessionEntity);
}