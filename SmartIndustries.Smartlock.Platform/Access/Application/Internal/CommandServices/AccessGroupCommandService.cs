using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartIndustries.Smartlock.Platform.Access.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Access.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Resources.Errors;

namespace SmartIndustries.Smartlock.Platform.Access.Application.Internal.CommandServices;

public class AccessGroupCommandService(
    IAccessGroupRepository accessGroupRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer) : IAccessGroupCommandService
{
    public async Task<Result<AccessGroup>> Handle(CreateAccessGroupCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var accessGroup = new AccessGroup(command.OrganizationId, command.Name, command.Description);
            await accessGroupRepository.AddAsync(accessGroup, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<AccessGroup>.Success(accessGroup);
        }
        catch (ArgumentException exception)
        {
            return Result<AccessGroup>.Failure(AccessError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<AccessGroup>.Failure(AccessError.OperationCancelled, localizer["AccessError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<AccessGroup>.Failure(AccessError.DatabaseError, localizer["AccessError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<AccessGroup>.Failure(AccessError.InternalServerError, localizer["AccessError.InternalServerError"]);
        }
    }
}
