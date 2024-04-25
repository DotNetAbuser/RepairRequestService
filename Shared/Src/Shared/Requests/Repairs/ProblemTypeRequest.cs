namespace Shared.Requests.Repairs;

public class ProblemTypeRequest
{
    public int EquipmentTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
}