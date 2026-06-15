using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
public static class ModelBuilderExtensions
{
    public static void ApplySpaceManagementConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            entity.OwnsOne(e => e.Name, name =>
            {
                name.WithOwner().HasForeignKey("Id");
                name.Property(n => n.Value).HasColumnName("name").IsRequired().HasMaxLength(100);
            });

            entity.Property(e => e.Description).HasMaxLength(500);
        });
    }
}
