using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Administration.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.Administration.Application.Internal.CommandServices;

public class MembershipCommandService(
    IMembershipRepository membershipRepository,
    IUnitOfWork unitOfWork) : IMembershipCommandService
{
    public async Task<Result<Membership>> Handle(AddRootUserToOrganizationCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var membership = new Membership(command.CreatorUserId, command.RoleId);
            await membershipRepository.AddAsync(membership, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Membership>.Success(membership);
        }
        catch (ArgumentException exception)
        {
            return Result<Membership>.Failure(AdministrationError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Membership>.Failure(AdministrationError.OperationCancelled, "Operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<Membership>.Failure(AdministrationError.DatabaseError, "Database error occurred.");
        }
        catch (Exception)
        {
            return Result<Membership>.Failure(AdministrationError.InternalServerError, "An internal error occurred.");
        }
    }
}
