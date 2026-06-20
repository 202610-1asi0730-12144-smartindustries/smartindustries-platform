using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.Entities;

namespace SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;

public partial class PersonAccess : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
