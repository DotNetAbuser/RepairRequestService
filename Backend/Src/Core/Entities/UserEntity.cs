namespace Core.Entities;

public class UserEntity : BaseEntity<Guid>
{
    public int RoleId { get; set; }

    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; } = string.Empty;


    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public bool IsActive { get; set; }
    
    public virtual RoleEntity Role { get; set; } = null!;
    public virtual List<RefreshSessionEntity> RefreshSessions { get; set; } = [];
    public virtual List<RepairRequestEntity> RepairRequests { get; set; } = [];
}