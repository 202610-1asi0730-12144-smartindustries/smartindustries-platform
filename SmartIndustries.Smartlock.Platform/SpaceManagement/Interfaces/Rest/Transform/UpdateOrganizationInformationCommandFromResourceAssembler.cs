using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;

public static class UpdateOrganizationInformationCommandFromResourceAssembler
{
    public static UpdateOrganizationInformationCommand ToCommandFromResource(UpdateOrganizationInformationResource resource, long organizationId)
        => new(organizationId, resource.Name, resource.Description);
}
