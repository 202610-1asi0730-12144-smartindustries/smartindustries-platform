using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;

namespace SmartIndustries.Smartlock.Platform.Access.Application.CommandServices;

public interface IPersonAccessCommandService
{
    Task<Result<PersonAccess>> Handle(CreatePersonAccessCommand command, CancellationToken cancellationToken = default);
}
