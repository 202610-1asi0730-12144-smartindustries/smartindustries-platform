using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

namespace SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;

public partial class AccessGroup
{
    public long Id { get; private set; }
    public long OrganizationId { get; private set; }
    public GenericName Name { get; private set; }
    public string Description { get; private set; }

    public AccessGroup(long organizationId, string name, string description)
    {
        OrganizationId = organizationId;
        Name = new GenericName(name);
        Description = description?.Trim() ?? string.Empty;
    }

    private AccessGroup() { }
}
