using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.Events;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Events;

public class OrganizationCreatedEvent(long organizationId, string name, long creatorUserId) : IEvent
{
    public long OrganizationId => organizationId;
    public string Name => name;
    public long CreatorUserId => creatorUserId;
}
