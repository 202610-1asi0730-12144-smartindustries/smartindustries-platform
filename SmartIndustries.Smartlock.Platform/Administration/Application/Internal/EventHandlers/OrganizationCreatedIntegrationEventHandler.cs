using Microsoft.Extensions.Logging;
using SmartIndustries.Smartlock.Platform.Administration.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Shared.Application.Internal.EventHandlers;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Events;

namespace SmartIndustries.Smartlock.Platform.Administration.Application.Internal.EventHandlers;

public class OrganizationCreatedIntegrationEventHandler(
    IRoleCommandService roleCommandService,
    ILogger<OrganizationCreatedIntegrationEventHandler> logger)
    : IEventHandler<OrganizationCreatedIntegrationEvent>
{
    public async Task Handle(OrganizationCreatedIntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        var result = await roleCommandService.Handle(
            new CreateRootRoleCommand(integrationEvent.OrganizationId, integrationEvent.CreatorUserId), cancellationToken);

        if (result.IsFailure)
            logger.LogWarning("Failed to create root role for organization {OrganizationId}: {Message}",
                integrationEvent.OrganizationId, result.Message);

        var basicResult = await roleCommandService.Handle(
            new CreateBasicRoleCommand(integrationEvent.OrganizationId), cancellationToken);

        if (basicResult.IsFailure)
            logger.LogWarning("Failed to create basic role for organization {OrganizationId}: {Message}",
                integrationEvent.OrganizationId, basicResult.Message);
    }
}
