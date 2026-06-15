using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;

public static class CreateOrganizationCommandFromResourceAssembler
{
    public static CreateOrganizationCommand ToCommandFromResource(CreateOrganizationResource resource, long creatorUserId)
        => new(resource.Name, resource.Description, creatorUserId);
}
