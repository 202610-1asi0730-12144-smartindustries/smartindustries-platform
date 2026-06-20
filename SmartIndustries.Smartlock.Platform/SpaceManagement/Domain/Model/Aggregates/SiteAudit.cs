using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.Entities;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;

public partial class Site : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
