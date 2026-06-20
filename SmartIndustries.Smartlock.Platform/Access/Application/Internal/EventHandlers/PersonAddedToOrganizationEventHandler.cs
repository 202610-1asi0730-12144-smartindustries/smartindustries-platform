using Microsoft.Extensions.Logging;
using SmartIndustries.Smartlock.Platform.Access.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Shared.Application.Internal.EventHandlers;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Events;

namespace SmartIndustries.Smartlock.Platform.Access.Application.Internal.EventHandlers;

public class PersonAddedToOrganizationEventHandler(
    IPersonAccessCommandService personAccessCommandService,
    ILogger<PersonAddedToOrganizationEventHandler> logger)
    : IEventHandler<PersonAddedToOrganizationIntegrationEvent>
{
    public async Task Handle(PersonAddedToOrganizationIntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        var command = new CreatePersonAccessCommand(integrationEvent.PersonId);
        var result = await personAccessCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
            logger.LogWarning("Failed to create person access for person {PersonId}: {Message}",
                integrationEvent.PersonId, result.Message);
    }
}
