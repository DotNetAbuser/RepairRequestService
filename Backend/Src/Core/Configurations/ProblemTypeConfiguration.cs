namespace Core.Configurations;

public class ProblemTypeConfiguration
    : IEntityTypeConfiguration<ProblemTypeEntity>
{
    public void Configure(EntityTypeBuilder<ProblemTypeEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder
            .HasOne(f => f.EquipmentType)
            .WithMany(s => s.ProblemTypes)
            .HasForeignKey(f => f.EquipmentTypeId);

        builder
            .HasMany(f => f.RepairRequests)
            .WithOne(s => s.ProblemType);

        var problemsType = new List<ProblemTypeEntity>
        {
            new()
            {
                Id = 1,
                EquipmentTypeId = 1,
                Name = "Не работает USB порт",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 2,
                EquipmentTypeId = 1,
                Name = "Не включается",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 3,
                EquipmentTypeId = 1,
                Name = "Другое",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 4,
                EquipmentTypeId = 2,
                Name = "Не идёт зарядка",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 5,
                EquipmentTypeId = 2,
                Name = "Разбит экран",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 6,
                EquipmentTypeId = 2,
                Name = "Другое",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 7,
                EquipmentTypeId = 3,
                Name = "Не идёт зарядка",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 8,
                EquipmentTypeId = 3,
                Name = "Не работают внешние кнопки",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 9,
                EquipmentTypeId = 3,
                Name = "Другое",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 10,
                EquipmentTypeId = 4,
                Name = "Не идёт звук из колонки",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 11,
                EquipmentTypeId = 4,
                Name = "Не работает Блютуз модуль",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 12,
                EquipmentTypeId = 4,
                Name = "Другое",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 13,
                EquipmentTypeId = 5,
                Name = "Не идёт звук из наушников",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 14,
                EquipmentTypeId = 5,
                Name = "Не работает Блютуз модуль",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 15,
                EquipmentTypeId = 5,
                Name = "Другое",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 16,
                EquipmentTypeId = 6,
                Name = "Не идёт звук из наушников",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 17,
                EquipmentTypeId = 6,
                Name = "Сломан 3.5  мм порт",
                CreatedOn = DateTime.UtcNow
            },
            new()
            {
                Id = 18,
                EquipmentTypeId = 6,
                Name = "Другое",
                CreatedOn = DateTime.UtcNow
            },
        };
        builder.HasData(problemsType);
    }
}