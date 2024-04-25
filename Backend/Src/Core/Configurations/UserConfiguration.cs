namespace Core.Configurations;

public class UserConfiguration(IPasswordHasher passwordHasher)
    : IEntityTypeConfiguration<UserEntity>
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder
            .HasIndex(f => f.Email)
            .IsUnique();

        builder
            .HasOne(f => f.Role)
            .WithMany(s => s.Users)
            .HasForeignKey(f => f.RoleId);

        builder
            .HasMany(f => f.RefreshSessions)
            .WithOne(s => s.User);

        builder
            .HasMany(f => f.RepairRequests)
            .WithOne(s => s.User);

        var users = new List<UserEntity>
        {
            new()
            {
                Id = Guid.NewGuid(),
                RoleId = (int)Role.Admin,
                LastName = "Фамилия",
                FirstName = "Имя",
                MiddleName = "Отчество",
                PasswordHash = _passwordHasher.Generate("12032003"),
                Email = "admin@example.com",
                IsActive = true,
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                RoleId = (int)Role.Guest,
                LastName = "Фамилия",
                FirstName = "Имя",
                MiddleName = "Отчество",
                PasswordHash = _passwordHasher.Generate("12032003"),
                Email = "guest@example.com",
                IsActive = true,
                CreatedOn = DateTime.UtcNow
            }
        };

        builder.HasData(users);
    }
}