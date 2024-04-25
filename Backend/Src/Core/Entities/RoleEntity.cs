namespace Core.Entities;

public class RoleEntity : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public virtual List<UserEntity> Users { get; set; } = [];
}