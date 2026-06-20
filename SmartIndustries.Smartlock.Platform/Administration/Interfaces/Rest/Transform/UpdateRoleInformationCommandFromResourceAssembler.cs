using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Transform;

public static class UpdateRoleInformationCommandFromResourceAssembler
{
    public static UpdateRoleInformationCommand ToCommandFromResource(UpdateRoleInformationResource resource, long roleId)
        => new(roleId, resource.Name, resource.CanCreateSites, resource.CanCreatePeople, resource.CanConnectDevices);
}
