using System.Collections;

namespace Core.IServices;

public interface IUserService
{
    Task<Result> RegisterUserAsync(RegisterRequest request);

    Task<Result<PaginatedData<UserResponse>>> GetPaginatedUsersAsync(int pageNumber, int pageSize,
        string searchTerm, string sortColumn, string sortOrder);

    Task<Result<UserResponse>> GetByIdAsync(string userId);
    Task<Result> ToggleUserStatusAsync(ToggleUserStatusRequest request);
    Task<Result<UserRolesResponse>> GetRolesByUserIdAsync(string userId);
    Task<Result> UpdateRoleAsync(string userId, UpdateUserRoleRequest request, string currentUserId);
}