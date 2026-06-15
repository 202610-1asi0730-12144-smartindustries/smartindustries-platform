using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.CommandServices;

public interface IOrganizationCommandService
{
    Task<Result<Organization>> Handle(CreateOrganizationCommand command, CancellationToken cancellationToken = default);
}
