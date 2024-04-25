namespace Core.Configurations;

public class EquipmentTypeConfiguration
    : IEntityTypeConfiguration<EquipmentTypeEntity>
{
    public void Configure(EntityTypeBuilder<EquipmentTypeEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder
            .HasIndex(f => f.Name)
            .IsUnique();

        builder
            .HasMany(f => f.ProblemTypes)
            .WithOne(s => s.EquipmentType);

        builder
            .HasMany(f => f.RepairRequests)
            .WithOne(s => s.EquipmentType);

        var equipmentTypes = new List<EquipmentTypeEntity>()
        {
            new()
            {
                Id = 1,
                Name = "Персональный ПК",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 2,
                Name = "Смартфоны",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 3,
                Name = "Умные часы",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 4,
                Name = "Умные колонки",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 5,
                Name = "Беспроводные наушники",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 6,
                Name = "Проводные наушники наушники",
                CreatedOn = DateTime.UtcNow
            },
        };

        builder.HasData(equipmentTypes);
    }
}