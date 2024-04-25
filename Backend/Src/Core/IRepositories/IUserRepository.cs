namespace Core.IRepositories;

public interface IUserRepository
{
    Task<UserEntity?> GetUserByEmailAsync(string email);
    Task<PaginatedData<UserEntity>> GetPaginatedUsersAsync(int pageNumber, int pageSize,
        string searchTerm, string sortColumn, string sortOrder);

    Task<UserEntity?> GetByUserIdAsync(Guid userId);
    Task<UserEntity?> GetByUserIdWithRoleIncludeAsync(Guid userId);
    Task<UserEntity?> GetUserByEmailWithRoleAsync(string email);
    Task CreateAsync(UserEntity userEntity);
    Task UpdateAsync(UserEntity userEntity);
    Task<bool> IsExistForUpdateByEmailAsync(Guid userId, string email);
}