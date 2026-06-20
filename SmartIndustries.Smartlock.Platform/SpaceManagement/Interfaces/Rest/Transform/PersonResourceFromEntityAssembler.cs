using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;

public static class PersonResourceFromEntityAssembler
{
    public static PersonResource ToResourceFromEntity(Person person)
        => new(person.Id, person.OrganizationId, person.Name.FirstName, person.Name.LastName, person.IdentityDocument.Value);
}
