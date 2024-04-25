namespace Core.Configurations;

public class RepairStatusConfiguration
    : IEntityTypeConfiguration<RepairStatusEntity>
{
    public void Configure(EntityTypeBuilder<RepairStatusEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder
            .HasIndex(f => f.Name)
            .IsUnique();

        builder
            .HasMany(f => f.RepairRequests)
            .WithOne(s => s.RepairStatus);

        var repairStatues = new List<RepairStatusEntity>
        {
            new()
            {
                Id = 1,
                Name = "В ожидание",

                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 2,
                Name = "В процессе",

                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 3,
                Name = "Выполнена",

                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 4,
                Name = "Отменена",

                CreatedOn = DateTime.UtcNow
            }
        };

        builder.HasData(repairStatues);
    }
}