using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;

namespace SmartIndustries.Smartlock.Platform.Administration.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAdministrationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.OrganizationId).IsRequired();
            entity.Property(e => e.Deletable).IsRequired();

            entity.OwnsOne(e => e.Name, name =>
            {
                name.WithOwner().HasForeignKey("Id");
                name.Property(n => n.Value).HasColumnName("name").IsRequired().HasMaxLength(100);
            });

            entity.OwnsOne(e => e.Permissions, permissions =>
            {
                permissions.WithOwner().HasForeignKey("Id");
                permissions.Property(p => p.CanCreateSites).HasColumnName("can_create_sites").IsRequired();
                permissions.Property(p => p.CanCreatePeople).HasColumnName("can_create_people").IsRequired();
                permissions.Property(p => p.CanConnectDevices).HasColumnName("can_connect_devices").IsRequired();
            });
        });
    }
    
}