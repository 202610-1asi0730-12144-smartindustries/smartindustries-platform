using Microsoft.Extensions.Logging;
using SmartIndustries.Smartlock.Platform.Administration.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Events;
using SmartIndustries.Smartlock.Platform.Shared.Application.Internal.EventHandlers;

namespace SmartIndustries.Smartlock.Platform.Administration.Application.Internal.EventHandlers;

public class RootRoleCreatedEventHandler(
    IMembershipCommandService membershipCommandService,
    ILogger<RootRoleCreatedEventHandler> logger)
    : IEventHandler<RootRoleCreatedEvent>
{
    public async Task Handle(RootRoleCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        var command = new AddRootUserToOrganizationCommand(domainEvent.CreatorUserId, domainEvent.RoleId);
        var result = await membershipCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
            logger.LogWarning("Failed to add root user to organization for role {RoleId}: {Message}",
                domainEvent.RoleId, result.Message);
    }
}
