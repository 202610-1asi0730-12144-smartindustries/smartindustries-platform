using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.CommandServices;

public interface ISiteCommandService
{
    Task<Result<Site>> Handle(AddSiteToOrganizationCommand command, CancellationToken cancellationToken = default);
    Task<Result<Site>> Handle(UpdateSiteInformationCommand command, CancellationToken cancellationToken = default);
}
