namespace Core.Configurations;

public class RepairRequestConfiguration
    : IEntityTypeConfiguration<RepairRequestEntity>
{
    public void Configure(EntityTypeBuilder<RepairRequestEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder
            .HasOne(f => f.User)
            .WithMany(s => s.RepairRequests)
            .HasForeignKey(f => f.UserId);

        builder
            .HasOne(f => f.EquipmentType)
            .WithMany(s => s.RepairRequests)
            .HasForeignKey(f => f.EquipmentTypeId);

        builder
            .HasOne(f => f.ProblemType)
            .WithMany(s => s.RepairRequests)
            .HasForeignKey(f => f.ProblemTypeId);

        builder
            .HasOne(f => f.RepairStatus)
            .WithMany(s => s.RepairRequests)
            .HasForeignKey(f => f.RepairStatusId);
    }
}