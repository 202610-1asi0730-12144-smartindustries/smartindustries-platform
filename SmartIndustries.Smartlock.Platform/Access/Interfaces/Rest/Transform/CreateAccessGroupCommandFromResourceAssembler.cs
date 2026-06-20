using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Access.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.Access.Interfaces.Rest.Transform;

public static class CreateAccessGroupCommandFromResourceAssembler
{
    public static CreateAccessGroupCommand ToCommandFromResource(CreateAccessGroupResource resource, long organizationId)
        => new(organizationId, resource.Name, resource.Description);
}
