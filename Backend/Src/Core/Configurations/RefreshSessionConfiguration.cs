namespace Core.Configurations;

public class RefreshSessionConfiguration
    : IEntityTypeConfiguration<RefreshSessionEntity>
{
    public void Configure(EntityTypeBuilder<RefreshSessionEntity> builder)
    {
        builder.HasKey(f => f.Id);

        builder
            .HasIndex(f => f.Token)
            .IsUnique();

        builder
            .HasOne(f => f.User)
            .WithMany(s => s.RefreshSessions)
            .HasForeignKey(f => f.UserId);
    }
}