using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;

public static class UpdateSiteInformationCommandFromResourceAssembler
{
    public static UpdateSiteInformationCommand ToCommandFromResource(UpdateSiteInformationResource resource, long siteId)
        => new(siteId, resource.Name, resource.Description);
}
