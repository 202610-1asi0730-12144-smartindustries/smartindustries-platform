using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;

public static class AddPersonToOrganizationCommandFromResourceAssembler
{
    public static AddPersonToOrganizationCommand ToCommandFromResource(AddPersonToOrganizationResource resource, long organizationId)
        => new(organizationId, resource.FirstName, resource.LastName, resource.IdentityDocument);
}
