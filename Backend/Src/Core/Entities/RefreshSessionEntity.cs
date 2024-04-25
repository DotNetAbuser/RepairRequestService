namespace Core.Entities;

public class RefreshSessionEntity : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }

    public UserEntity User { get; set; } = null!;
}