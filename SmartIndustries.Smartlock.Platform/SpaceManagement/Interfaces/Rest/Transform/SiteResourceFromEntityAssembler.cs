using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;

public static class SiteResourceFromEntityAssembler
{
    public static SiteResource ToResourceFromEntity(Site site)
        => new(site.Id, site.OrganizationId, site.Name.Value, site.Description);
}
