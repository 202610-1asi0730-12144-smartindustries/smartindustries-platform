using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Report.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;

namespace SmartIndustries.Smartlock.Platform.Report.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyReportConfiguration(this ModelBuilder builder)
    {
        builder.Entity<ScheduleDay>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.PersonId).IsRequired();
            entity.HasOne<Person>()
                .WithMany()
                .HasForeignKey(e => e.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.Day).HasConversion<string>().IsRequired().HasMaxLength(50);

            entity.OwnsOne(e => e.TimeBlock, time =>
            {
                time.WithOwner().HasForeignKey("Id");
                time.Property(t => t.Start).HasColumnName("start");
                time.Property(t => t.End).HasColumnName("end");
            });
        });
    }
}
