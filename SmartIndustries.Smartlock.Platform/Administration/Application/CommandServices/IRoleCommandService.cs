using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;

namespace SmartIndustries.Smartlock.Platform.Administration.Application.CommandServices;

public interface IRoleCommandService
{
    Task<Result<Role>> Handle(CreateRootRoleCommand command, CancellationToken cancellationToken = default);
    Task<Result<Role>> Handle(CreateBasicRoleCommand command, CancellationToken cancellationToken = default);
}
