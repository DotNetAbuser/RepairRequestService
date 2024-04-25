namespace Client.Infrastructure.Routes.Repair;

public static class RepairStatusRoutes
{
    public static string GetAllStatuses = "api/repair/repair_status";
    public static string Create = "api/repair/repair_status";
    
    public static string GetById(string statusRepairRequestId) =>
        $"api/repair/repair_status/{statusRepairRequestId}";
    public static string Update(string statusRepairRequestId) =>
        $"api/repair/repair_status/{statusRepairRequestId}";
    public static string Delete(string statusRepairRequestId) =>
        $"api/repair/repair_status/{statusRepairRequestId}";

}