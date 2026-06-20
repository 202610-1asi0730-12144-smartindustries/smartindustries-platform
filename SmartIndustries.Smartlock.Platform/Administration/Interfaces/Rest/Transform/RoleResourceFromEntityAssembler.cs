using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Transform;

public static class RoleResourceFromEntityAssembler
{
    public static RoleResource ToResourceFromEntity(Role role)
        => new(role.Id, role.OrganizationId, role.Name.Value, role.Permissions.CanCreateSites, role.Permissions.CanCreatePeople, role.Permissions.CanConnectDevices, role.Deletable);
}
