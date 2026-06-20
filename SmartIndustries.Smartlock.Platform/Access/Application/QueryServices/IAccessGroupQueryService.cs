using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Queries;

namespace SmartIndustries.Smartlock.Platform.Access.Application.QueryServices;

public interface IAccessGroupQueryService
{
    Task<IEnumerable<AccessGroup>> Handle(GetAccessGroupsByOrganizationIdQuery query, CancellationToken cancellationToken = default);
}
