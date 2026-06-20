using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;

namespace SmartIndustries.Smartlock.Platform.Access.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAccessConfiguration(this ModelBuilder builder)
    {
        builder.Entity<AccessGroup>(entity =>
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

        builder.Entity<PersonAccess>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.PersonId).IsRequired();
            entity.HasOne<Person>()
                .WithMany()
                .HasForeignKey(e => e.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => e.PersonId).IsUnique();
            entity.Property(e => e.Status).HasConversion<string>().IsRequired().HasMaxLength(50);
        });
    }
}