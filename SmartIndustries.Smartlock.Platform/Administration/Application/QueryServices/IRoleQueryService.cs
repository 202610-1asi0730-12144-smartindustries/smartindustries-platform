using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Queries;

namespace SmartIndustries.Smartlock.Platform.Administration.Application.QueryServices;

public interface IRoleQueryService
{
    Task<IEnumerable<Role>> Handle(GetRolesByOrganizationIdQuery query, CancellationToken cancellationToken = default);
}
