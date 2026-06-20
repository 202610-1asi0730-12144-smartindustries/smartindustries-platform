using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Access.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.Access.Interfaces.Rest.Transform;

public static class AccessGroupResourceFromEntityAssembler
{
    public static AccessGroupResource ToResourceFromEntity(AccessGroup accessGroup)
        => new(accessGroup.Id, accessGroup.OrganizationId, accessGroup.Name.Value, accessGroup.Description);
}
