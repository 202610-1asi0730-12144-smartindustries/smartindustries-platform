using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;

namespace SmartIndustries.Smartlock.Platform.Administration.Application.CommandServices;

public interface IMembershipCommandService
{
    Task<Result<Membership>> Handle(AddRootUserToOrganizationCommand command, CancellationToken cancellationToken = default);
    Task<Result<Membership>> Handle(UpdateUserRoleInOrganizationCommand command, CancellationToken cancellationToken = default);
}
