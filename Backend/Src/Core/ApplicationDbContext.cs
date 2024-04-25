using Core.Configurations;

namespace Core;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IPasswordHasher passwordHasher)
    : DbContext(options)
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public DbSet<EquipmentTypeEntity> EquipmentTypes { get; set; }
    public DbSet<ProblemTypeEntity> ProblemTypes { get; set; }
    public DbSet<RepairStatusEntity> RepairStatuses { get; set; }
    public DbSet<RepairRequestEntity> RepairRequests { get; set; }

    public DbSet<RefreshSessionEntity> RefreshSessions { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new UserConfiguration(_passwordHasher));
    }
}