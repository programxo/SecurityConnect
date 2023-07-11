namespace SecurityConnect.Infrastructure.Persistence.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedNever();

            builder.Property(u => u.FirstName)
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .HasMaxLength(100);

            builder.Property(u => u.UserRole)
                .HasMaxLength(10);

            builder.HasOne(u => u.ManagedByAdmin)
                .WithMany()
                .HasForeignKey(u => u.ManagedByAdminId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
