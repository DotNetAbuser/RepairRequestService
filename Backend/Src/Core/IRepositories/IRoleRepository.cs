using System.Collections;

namespace Core.IRepositories;

public interface IRoleRepository
{
    Task<IEnumerable<RoleEntity>> GetAllAsync();
    Task<RoleEntity?> GetByNameAsync(string name);
}