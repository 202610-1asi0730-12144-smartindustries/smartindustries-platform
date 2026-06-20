using Cortex.Mediator;
using SmartIndustries.Smartlock.Platform.Shared.Application.Internal.EventHandlers;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Events;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Events;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.Internal.EventHandlers;

public class PersonAddedToOrganizationEventHandler(IMediator mediator)
    : IEventHandler<PersonAddedToOrganizationEvent>
{
    public async Task Handle(PersonAddedToOrganizationEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new PersonAddedToOrganizationIntegrationEvent(domainEvent.PersonId);
        await mediator.PublishAsync(integrationEvent, cancellationToken);
    }
}
