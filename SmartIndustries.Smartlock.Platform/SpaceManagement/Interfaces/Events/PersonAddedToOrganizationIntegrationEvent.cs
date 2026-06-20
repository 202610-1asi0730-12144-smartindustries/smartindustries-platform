using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.Events;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Events;

public record PersonAddedToOrganizationIntegrationEvent(long PersonId) : IEvent;
