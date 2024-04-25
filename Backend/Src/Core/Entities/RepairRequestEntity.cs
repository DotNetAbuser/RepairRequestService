namespace Core.Entities;

public class RepairRequestEntity : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    
    public int EquipmentTypeId { get; set; }
    public int ProblemTypeId { get; set; }
    public int RepairStatusId { get; set; }

    public string ProblemDescription { get; set; } = string.Empty;

    public UserEntity User { get; set; } = null!;

    public EquipmentTypeEntity EquipmentType { get; set; } = null!;
    public ProblemTypeEntity ProblemType { get; set; } = null!;
    public RepairStatusEntity RepairStatus { get; set; } = null!;
}