using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.CommandServices;

public interface IPersonCommandService
{
    Task<Result<Person>> Handle(AddPersonToOrganizationCommand command, CancellationToken cancellationToken = default);
}
