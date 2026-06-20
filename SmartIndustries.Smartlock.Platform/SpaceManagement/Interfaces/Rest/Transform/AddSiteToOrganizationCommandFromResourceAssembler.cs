using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;

public static class AddSiteToOrganizationCommandFromResourceAssembler
{
    public static AddSiteToOrganizationCommand ToCommandFromResource(AddSiteToOrganizationResource resource, long organizationId)
        => new(organizationId, resource.Name, resource.Description);
}
