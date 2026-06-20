using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartIndustries.Smartlock.Platform.Administration.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Resources.Errors;

namespace SmartIndustries.Smartlock.Platform.Administration.Application.Internal.CommandServices;

public class MembershipCommandService(
    IMembershipRepository membershipRepository,
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer) : IMembershipCommandService
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
            return Result<Membership>.Failure(AdministrationError.OperationCancelled, localizer["AdministrationError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<Membership>.Failure(AdministrationError.DatabaseError, localizer["AdministrationError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<Membership>.Failure(AdministrationError.InternalServerError, localizer["AdministrationError.InternalServerError"]);
        }
    }

    public async Task<Result<Membership>> Handle(UpdateUserRoleInOrganizationCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var membership = await membershipRepository.FindByUserIdAsync(command.UserId, cancellationToken);
            if (membership == null)
                return Result<Membership>.Failure(AdministrationError.MembershipNotFound,
                    localizer["AdministrationError.MembershipNotFound"]);

            var currentRole = await roleRepository.FindByIdAsync(membership.RoleId, cancellationToken);
            if (currentRole == null || currentRole.OrganizationId != command.OrganizationId)
                return Result<Membership>.Failure(AdministrationError.MembershipNotFound,
                    localizer["AdministrationError.MembershipNotFound"]);

            var newRole = await roleRepository.FindByIdAsync(command.NewRoleId, cancellationToken);
            if (newRole == null || newRole.OrganizationId != command.OrganizationId)
                return Result<Membership>.Failure(AdministrationError.RoleNotFound,
                    localizer["AdministrationError.RoleNotFound"]);

            membership.UpdateRole(command.NewRoleId);
            membershipRepository.Update(membership);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Membership>.Success(membership);
        }
        catch (ArgumentException exception)
        {
            return Result<Membership>.Failure(AdministrationError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Membership>.Failure(AdministrationError.OperationCancelled, localizer["AdministrationError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<Membership>.Failure(AdministrationError.DatabaseError, localizer["AdministrationError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<Membership>.Failure(AdministrationError.InternalServerError, localizer["AdministrationError.InternalServerError"]);
        }
    }
}
