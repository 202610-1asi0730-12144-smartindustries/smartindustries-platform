using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;

namespace SmartIndustries.Smartlock.Platform.Access.Application.CommandServices;

public interface IAccessGroupCommandService
{
    Task<Result<AccessGroup>> Handle(CreateAccessGroupCommand command, CancellationToken cancellationToken = default);
}
