using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Transform;

public static class AddRoleToOrganizationCommandFromResourceAssembler
{
    public static AddRoleToOrganizationCommand ToCommandFromResource(AddRoleToOrganizationResource resource, long organizationId)
        => new(organizationId, resource.Name, resource.CanCreateSites, resource.CanCreatePeople, resource.CanConnectDevices);
}
