using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Aggregates;

namespace SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();

            entity.OwnsOne(e => e.Name, name =>
            {
                name.WithOwner().HasForeignKey("Id");
                name.Property(n => n.FirstName).HasColumnName("first_name").IsRequired();
                name.Property(n => n.LastName).HasColumnName("last_name").IsRequired();
            });

            entity.Property(e => e.PasswordHash).HasColumnName("password").IsRequired();

            entity.OwnsOne(e => e.Email, email =>
            {
                email.WithOwner().HasForeignKey("Id");
                email.Property(e => e.Value).HasColumnName("email").IsRequired();
                email.HasIndex(e => e.Value).IsUnique();
            });
        });
        
    }
}
