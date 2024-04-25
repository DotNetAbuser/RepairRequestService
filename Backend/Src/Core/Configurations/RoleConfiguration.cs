namespace Core.Configurations;

public class RoleConfiguration
    : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder
            .HasIndex(f => f.Name)
            .IsUnique();

        builder
            .HasIndex(f => f.Description)
            .IsUnique();

        builder
            .HasMany(f => f.Users)
            .WithOne(s => s.Role);

        var roles = Enum
            .GetValues<Role>()
            .Select(r => new RoleEntity
            {
                Id = (int)r,
                Name = r.ToString().ToLower(),
                Description = r.ToString().ToLower(),

                CreatedOn = DateTime.UtcNow
            });

        builder.HasData(roles);
    }
}