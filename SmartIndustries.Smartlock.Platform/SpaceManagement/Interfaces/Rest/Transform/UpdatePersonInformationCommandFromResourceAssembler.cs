using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;

public static class UpdatePersonInformationCommandFromResourceAssembler
{
    public static UpdatePersonInformationCommand ToCommandFromResource(UpdatePersonInformationResource resource, long personId)
        => new(personId, resource.FirstName, resource.LastName, resource.IdentityDocument);
}
