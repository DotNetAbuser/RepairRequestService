namespace Client.Infrastructure.Routes.Repair;

public static class RequestRoutes
{
    public static string Create = "api/repair/request";
    
    public static string GetPaginatedAll(int pageNumber, int pageSize, 
        string? searchTerm, string? sortColumn, string? sortOrder) =>
        "api/repair/request" +
        $"?pageNumber={pageNumber}" +
        $"&pageSize={pageSize}" +
        $"&searchTerm={searchTerm}" +
        $"&sortColumn={sortColumn}" +
        $"&sortOrder={sortOrder}";
    
    public static string GetPaginatedAllRequestsByUserId(string userId, int pageNumber, int pageSize,
        string? searchTerm, string? sortColumn, string? sortOrder) =>
        $"api/repair/request/user/{userId}" +
        $"?pageNumber={pageNumber}" +
        $"&pageSize={pageSize}" +
        $"&searchTerm={searchTerm}" +
        $"&sortColumn={sortColumn}" +
        $"&sortOrder={sortOrder}";
    
    public static string GetById(string repairRequestId) =>
        $"api/repair/request/{repairRequestId}";

    public static string Update(string repairRequestId) =>
        $"api/repair/request/{repairRequestId}";

    public static string ToggleStatusRequest(string repairRequestId) =>
        $"api/repair/request/status/{repairRequestId}";
}