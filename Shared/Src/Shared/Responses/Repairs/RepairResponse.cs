namespace Shared.Responses.Repairs;

public class RepairResponse
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string EquipmentType { get; set; } = string.Empty;
    public string ProblemType { get; set; } = string.Empty;
    public string RepairStatus { get; set; } = string.Empty;
    public string ProblemDescription { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}