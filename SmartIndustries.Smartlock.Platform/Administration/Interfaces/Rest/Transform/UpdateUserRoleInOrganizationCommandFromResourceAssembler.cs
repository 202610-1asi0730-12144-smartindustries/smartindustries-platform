using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Transform;

public static class UpdateUserRoleInOrganizationCommandFromResourceAssembler
{
    public static UpdateUserRoleInOrganizationCommand ToCommandFromResource(UpdateUserRoleInOrganizationResource resource, long userId, long organizationId)
        => new(userId, organizationId, resource.NewRoleId);
}
