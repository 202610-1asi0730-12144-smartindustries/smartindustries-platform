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

        builder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.OrganizationId).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.OwnsOne(e => e.Name, name =>
            {
                name.WithOwner().HasForeignKey("Id");
                name.Property(n => n.Value).HasColumnName("name").IsRequired().HasMaxLength(100);
            });
        });

        builder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.SiteId).IsRequired();
            entity.HasOne<Site>()
                .WithMany()
                .HasForeignKey(device => device.SiteId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.Status).HasConversion<string>().IsRequired().HasMaxLength(50);
            entity.Property(e => e.Mode).HasConversion<string>().IsRequired().HasMaxLength(50);

            entity.OwnsOne(e => e.Name, name =>
            {
                name.WithOwner().HasForeignKey("Id");
                name.Property(n => n.Value).HasColumnName("name").IsRequired().HasMaxLength(100);
            });
        });

        builder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.OrganizationId).IsRequired();

            entity.OwnsOne(e => e.Name, name =>
            {
                name.WithOwner().HasForeignKey("Id");
                name.Property(n => n.FirstName).HasColumnName("first_name").IsRequired().HasMaxLength(100);
                name.Property(n => n.LastName).HasColumnName("last_name").IsRequired().HasMaxLength(100);
            });

            entity.OwnsOne(e => e.IdentityDocument, document =>
            {
                document.WithOwner().HasForeignKey("Id");
                document.Property(d => d.Value).HasColumnName("identity_document").IsRequired().HasMaxLength(50);
            });
        });
    }
}
