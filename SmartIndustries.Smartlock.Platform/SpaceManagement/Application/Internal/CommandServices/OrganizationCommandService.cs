using Cortex.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Resources.Errors;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Events;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.Internal.CommandServices;

public class OrganizationCommandService(
    IOrganizationRepository organizationRepository,
    IUnitOfWork unitOfWork,
    IMediator mediator,
    IStringLocalizer<ErrorMessages> localizer) : IOrganizationCommandService
{
    public async Task<Result<Organization>> Handle(CreateOrganizationCommand command, CancellationToken cancellationToken = default)
    {
        if (await organizationRepository.ExistsByNameAsync(command.Name, cancellationToken))
            return Result<Organization>.Failure(
                SpaceManagementError.OrganizationAlreadyExists,
                localizer["SpaceManagementError.OrganizationAlreadyExists"]);

        try
        {
            var organization = new Organization(command.Name, command.Description);
            await organizationRepository.AddAsync(organization, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            await mediator.PublishAsync(
                new OrganizationCreatedEvent(organization.Id, organization.Name.Value, command.CreatorUserId), cancellationToken);

            return Result<Organization>.Success(organization);
        }
        catch (ArgumentException exception)
        {
            return Result<Organization>.Failure(SpaceManagementError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Organization>.Failure(SpaceManagementError.OperationCancelled, localizer["SpaceManagementError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<Organization>.Failure(SpaceManagementError.DatabaseError, localizer["SpaceManagementError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<Organization>.Failure(SpaceManagementError.InternalServerError, localizer["SpaceManagementError.InternalServerError"]);
        }
    }
}
