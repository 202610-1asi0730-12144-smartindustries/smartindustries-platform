using Cortex.Mediator;
using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Administration.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Events;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.Administration.Application.Internal.CommandServices;

public class RoleCommandService(
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork,
    IMediator mediator) : IRoleCommandService
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
            return Result<Role>.Failure(AdministrationError.OperationCancelled, "Operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<Role>.Failure(AdministrationError.DatabaseError, "Database error occurred.");
        }
        catch (Exception)
        {
            return Result<Role>.Failure(AdministrationError.InternalServerError, "An internal error occurred.");
        }
    }
}
