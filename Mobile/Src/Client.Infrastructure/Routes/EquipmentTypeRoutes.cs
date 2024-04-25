namespace Client.Infrastructure.Routes.Repair;

public static class EquipmentTypeRoutes
{
    public static string GetAll = "api/repair/equipment_type";
    public static string Create = "api/repair/equipment_type";
    
    public static string GetById(string equipmentTypeId) =>
        $"api/repair/equipment_type/{equipmentTypeId}";
    public static string Update(string equipmentTypeId) =>
        $"api/repair/equipment_type/{equipmentTypeId}";
    public static string Delete(string equipmentTypeId) =>
        $"api/repair/equipment_type/{equipmentTypeId}";
}