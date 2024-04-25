namespace Client.Infrastructure.Routes.Repair;

public static class ProblemTypeRoutes
{
    public static string Create = "api/repair/problem_type";
    
    public static string GetAllByEquipmentTypeId(string equipmentTypeId) =>
        $"api/repair/problem_type/equipment_type/{equipmentTypeId}";
    public static string GetById(string problemTypeId) =>
        $"api/repair/problem_type/{problemTypeId}";
    public static string Update(string problemTypeId) =>
        $"api/repair/problem_type/{problemTypeId}";
    public static string Delete(string problemTypeId) =>
        $"api/repair/problem_type/{problemTypeId}";
}