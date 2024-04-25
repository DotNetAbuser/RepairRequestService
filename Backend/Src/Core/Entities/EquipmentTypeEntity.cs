namespace Core.Entities;

public class EquipmentTypeEntity : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;

    public List<ProblemTypeEntity> ProblemTypes { get; set; } = [];
    public List<RepairRequestEntity> RepairRequests { get; set; } = [];
}