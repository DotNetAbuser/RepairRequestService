namespace Core.Entities;

public class RepairStatusEntity : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;

    public virtual List<RepairRequestEntity> RepairRequests { get; set; } = [];
}