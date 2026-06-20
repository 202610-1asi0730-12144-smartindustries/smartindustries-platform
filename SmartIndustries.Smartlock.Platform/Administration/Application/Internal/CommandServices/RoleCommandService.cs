using Cortex.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartIndustries.Smartlock.Platform.Administration.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Events;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.ValueObjects;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Resources.Errors;

namespace SmartIndustries.Smartlock.Platform.Administration.Application.Internal.CommandServices;

public class RoleCommandService(
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork,
    IMediator mediator,
    IStringLocalizer<ErrorMessages> localizer) : IRoleCommandService
{
    public async Task<Result<Role>> Handle(CreateRootRoleCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var role = Role.CreateRoot(command.OrganizationId);
            await roleRepository.AddAsync(role, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            await mediator.PublishAsync(
                new RootRoleCreatedEvent(role.Id, command.CreatorUserId), cancellationToken);

            return Result<Role>.Success(role);
        }
        catch (ArgumentException exception)
        {
            return Result<Role>.Failure(AdministrationError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Role>.Failure(AdministrationError.OperationCancelled, localizer["AdministrationError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<Role>.Failure(AdministrationError.DatabaseError, localizer["AdministrationError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<Role>.Failure(AdministrationError.InternalServerError, localizer["AdministrationError.InternalServerError"]);
        }
    }

    public async Task<Result<Role>> Handle(CreateBasicRoleCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var role = Role.CreateBasic(command.OrganizationId);
            await roleRepository.AddAsync(role, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Role>.Success(role);
        }
        catch (ArgumentException exception)
        {
            return Result<Role>.Failure(AdministrationError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Role>.Failure(AdministrationError.OperationCancelled, localizer["AdministrationError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<Role>.Failure(AdministrationError.DatabaseError, localizer["AdministrationError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<Role>.Failure(AdministrationError.InternalServerError, localizer["AdministrationError.InternalServerError"]);
        }
    }

    public async Task<Result<Role>> Handle(AddRoleToOrganizationCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var permissions = new RolePermissions(command.CanCreateSites, command.CanCreatePeople, command.CanConnectDevices);
            var role = Role.CreateCustom(command.OrganizationId, command.Name, permissions);
            await roleRepository.AddAsync(role, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Role>.Success(role);
        }
        catch (ArgumentException exception)
        {
            return Result<Role>.Failure(AdministrationError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Role>.Failure(AdministrationError.OperationCancelled, localizer["AdministrationError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<Role>.Failure(AdministrationError.DatabaseError, localizer["AdministrationError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<Role>.Failure(AdministrationError.InternalServerError, localizer["AdministrationError.InternalServerError"]);
        }
    }
}
