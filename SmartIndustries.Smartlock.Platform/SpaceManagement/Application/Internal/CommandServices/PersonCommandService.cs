using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Resources.Errors;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.Internal.CommandServices;

public class PersonCommandService(
    IPersonRepository personRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer) : IPersonCommandService
{
    public async Task<Result<Person>> Handle(AddPersonToOrganizationCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var person = new Person(command.OrganizationId, command.FirstName, command.LastName, command.IdentityDocument);
            await personRepository.AddAsync(person, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Person>.Success(person);
        }
        catch (ArgumentException exception)
        {
            return Result<Person>.Failure(SpaceManagementError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Person>.Failure(SpaceManagementError.OperationCancelled, localizer["SpaceManagementError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<Person>.Failure(SpaceManagementError.DatabaseError, localizer["SpaceManagementError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<Person>.Failure(SpaceManagementError.InternalServerError, localizer["SpaceManagementError.InternalServerError"]);
        }
    }
}
