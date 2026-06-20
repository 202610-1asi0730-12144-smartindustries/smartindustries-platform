using Microsoft.Extensions.Logging;
using SmartIndustries.Smartlock.Platform.Report.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Report.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Shared.Application.Internal.EventHandlers;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Events;

namespace SmartIndustries.Smartlock.Platform.Report.Application.Internal.EventHandlers;

public class PersonAddedToOrganizationEventHandler(
    IScheduleDayCommandService scheduleDayCommandService,
    ILogger<PersonAddedToOrganizationEventHandler> logger)
    : IEventHandler<PersonAddedToOrganizationIntegrationEvent>
{
    public async Task Handle(PersonAddedToOrganizationIntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        foreach (Day day in Enum.GetValues<Day>())
        {
            var command = new CreateScheduleDayCommand(integrationEvent.PersonId, day, new TimeBlock(null, null));
            var result = await scheduleDayCommandService.Handle(command, cancellationToken);

            if (result.IsFailure)
                logger.LogWarning("Failed to create schedule day {Day} for person {PersonId}: {Message}",
                    day, integrationEvent.PersonId, result.Message);
        }
    }
}
