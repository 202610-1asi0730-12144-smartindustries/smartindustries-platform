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

public class PersonAccessCommandService(
    IPersonAccessRepository personAccessRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer) : IPersonAccessCommandService
{
    public async Task<Result<PersonAccess>> Handle(CreatePersonAccessCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var personAccess = new PersonAccess(command.PersonId, 0);
            await personAccessRepository.AddAsync(personAccess, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<PersonAccess>.Success(personAccess);
        }
        catch (ArgumentException exception)
        {
            return Result<PersonAccess>.Failure(AccessError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<PersonAccess>.Failure(AccessError.OperationCancelled, localizer["AccessError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<PersonAccess>.Failure(AccessError.DatabaseError, localizer["AccessError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<PersonAccess>.Failure(AccessError.InternalServerError, localizer["AccessError.InternalServerError"]);
        }
    }
}
