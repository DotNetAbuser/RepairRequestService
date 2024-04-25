namespace Client.Infrastructure.Routes.Identity;

public static class UserRoutes
{
    public static string RegisterUser = "api/identity/user";
    public static string ToggleUserStatus = "api/identity/user/toggle-status";
    public static string GetPaginatedUsers(int pageNumber, int pageSize,
        string? searchTerm, string? sortColumn, string? sortOrder) =>
        $"api/identity/user" +
            $"?pageNumber={pageNumber}" +
            $"&pageSize={pageSize}" +
            $"&searchTerm={searchTerm}" +
            $"&sortColumn={sortColumn}" +
            $"&sortOrder={sortOrder}";
    public static string GetById(string userId) =>
        $"api/identity/user/{userId}";
    public static string GetRolesByUserId(string userId) =>
        $"api/identity/user/role/{userId}";
    public static string UpdateRolesByUserId(string userId) =>
        $"api/identity/user/role/{userId}";
}