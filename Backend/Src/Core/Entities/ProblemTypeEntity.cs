namespace Core.Entities;

public class ProblemTypeEntity : BaseEntity<int>
{
    public int EquipmentTypeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public EquipmentTypeEntity EquipmentType { get; set; } = null!;
    public List<RepairRequestEntity> RepairRequests { get; set; } = [];
}