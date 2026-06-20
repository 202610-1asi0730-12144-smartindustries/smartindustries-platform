using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.Entities;

namespace SmartIndustries.Smartlock.Platform.Report.Domain.Model.Aggregates;

public partial class ScheduleDay : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
