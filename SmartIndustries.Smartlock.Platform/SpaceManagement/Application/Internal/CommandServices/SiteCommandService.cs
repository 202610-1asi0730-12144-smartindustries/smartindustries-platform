using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.Internal.CommandServices;

public class SiteCommandService(
    ISiteRepository siteRepository,
    IUnitOfWork unitOfWork) : ISiteCommandService
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
            return Result<Site>.Failure(SpaceManagementError.OperationCancelled, "Operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<Site>.Failure(SpaceManagementError.DatabaseError, "Database error occurred.");
        }
        catch (Exception)
        {
            return Result<Site>.Failure(SpaceManagementError.InternalServerError, "An internal error occurred.");
        }
    }
}
