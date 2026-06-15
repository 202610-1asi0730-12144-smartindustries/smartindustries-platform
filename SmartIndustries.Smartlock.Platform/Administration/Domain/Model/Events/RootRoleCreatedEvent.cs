using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.Events;

namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Events;

public class RootRoleCreatedEvent(long roleId, long creatorUserId) : IEvent
{
    public long RoleId => roleId;
    public long CreatorUserId => creatorUserId;
}
