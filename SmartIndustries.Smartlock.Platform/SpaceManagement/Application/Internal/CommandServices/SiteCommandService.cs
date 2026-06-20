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

public class SiteCommandService(
    ISiteRepository siteRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer) : ISiteCommandService
{
    public async Task<Result<Site>> Handle(AddSiteToOrganizationCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var site = new Site(command.OrganizationId, command.Name, command.Description);
            await siteRepository.AddAsync(site, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Site>.Success(site);
        }
        catch (ArgumentException exception)
        {
            return Result<Site>.Failure(SpaceManagementError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Site>.Failure(SpaceManagementError.OperationCancelled, localizer["SpaceManagementError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<Site>.Failure(SpaceManagementError.DatabaseError, localizer["SpaceManagementError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<Site>.Failure(SpaceManagementError.InternalServerError, localizer["SpaceManagementError.InternalServerError"]);
        }
    }
}
