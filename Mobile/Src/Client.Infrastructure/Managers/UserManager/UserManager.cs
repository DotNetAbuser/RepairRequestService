using Shared.Responses;

namespace Client.Infrastructure.Managers.UserManager;

public class UserManager(IHttpClientFactory factory)
    : IUserManager
{
    private readonly IHttpClientFactory _factory = factory;
    
    public async Task<IResult<PaginatedData<UserResponse>>> GetPaginatedUsersAsync(int pageNumber, int pageSize,
        string? searchTerm, string? sortColumn, string? sortOrder)
    {
        var response = await _factory.CreateClient("Client").GetAsync(UserRoutes.GetPaginatedUsers(pageNumber, pageSize,
            searchTerm, sortColumn, sortOrder));
        var result = await response.ToResult<PaginatedData<UserResponse>>();
        return result;
    }

    public async Task<IResult<UserResponse>> GetByIdAsync(string userId)
    {
        var response = await _factory.CreateClient("Client").GetAsync(UserRoutes.GetById(userId));
        var result = await response.ToResult<UserResponse>();
        return result;
    }

    public async Task<IResult> RegisterUserAsync(RegisterRequest request)
    {
        var response = await _factory.CreateClient("Client").PostAsJsonAsync(UserRoutes.RegisterUser, request);
        var result = await response.ToResult();
        return result;
    }

    public async Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
    {
        var response = await _factory.CreateClient("Client").PostAsJsonAsync(UserRoutes.ToggleUserStatus, request);
        var result = await response.ToResult();
        return result;
    }

    public async Task<IResult<UserRolesResponse>> GetRolesByUserIdAsync(string userId)
    {
        var response = await _factory.CreateClient("Client").GetAsync(UserRoutes.GetRolesByUserId(userId));
        var result = await response.ToResult<UserRolesResponse>();
        return result;
    }

    public async Task<IResult> UpdateRolesByUserIdAsync(string userId, UpdateUserRoleRequest request)
    {
        var response = await _factory.CreateClient("Client").PutAsJsonAsync(UserRoutes.UpdateRolesByUserId(userId), request);
        var result = await response.ToResult();
        return result;
    }
}