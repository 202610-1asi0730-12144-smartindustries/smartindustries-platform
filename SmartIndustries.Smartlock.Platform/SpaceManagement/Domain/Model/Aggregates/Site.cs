using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;

public partial class Site
{
    public long Id { get; private set; }
    public long OrganizationId { get; private set; }
    public GenericName Name { get; private set; }
    public string Description { get; private set; }

    public Site(long organizationId, string name, string description)
    {
        OrganizationId = organizationId;
        Name = new GenericName(name);
        Description = description?.Trim() ?? string.Empty;
    }

    private Site() { }
}
