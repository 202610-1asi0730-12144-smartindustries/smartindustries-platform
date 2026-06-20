using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.Events;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Events;

public class PersonAddedToOrganizationEvent(long personId, string fullName, string identityDocument) : IEvent
{
    public long PersonId => personId;
    public string FullName => fullName;
    public string IdentityDocument => identityDocument;
}
