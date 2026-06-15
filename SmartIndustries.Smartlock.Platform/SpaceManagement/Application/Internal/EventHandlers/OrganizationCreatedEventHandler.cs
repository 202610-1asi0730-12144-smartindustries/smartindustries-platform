using Cortex.Mediator;
using SmartIndustries.Smartlock.Platform.Shared.Application.Internal.EventHandlers;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Events;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Events;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.Internal.EventHandlers;

public class OrganizationCreatedEventHandler(IMediator mediator)
    : IEventHandler<OrganizationCreatedEvent>
{
    public async Task Handle(OrganizationCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new OrganizationCreatedIntegrationEvent(
            domainEvent.OrganizationId,
            domainEvent.Name,
            domainEvent.CreatorUserId);

        await mediator.PublishAsync(integrationEvent, cancellationToken);
    }
}
