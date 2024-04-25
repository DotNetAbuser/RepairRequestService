namespace Client.Infrastructure.Managers.UserManager;

public interface IUserManager
{
    Task<IResult<PaginatedData<UserResponse>>> GetPaginatedUsersAsync(int pageNumber, int pageSize,
        string? searchTerm, string? sortColumn, string? sortOrder);
    Task<IResult<UserResponse>> GetByIdAsync(string userId);
    Task<IResult> RegisterUserAsync(RegisterRequest request);
    Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request);
    Task<IResult<UserRolesResponse>> GetRolesByUserIdAsync(string userId);
    Task<IResult> UpdateRolesByUserIdAsync(string userId, UpdateUserRoleRequest request);
}