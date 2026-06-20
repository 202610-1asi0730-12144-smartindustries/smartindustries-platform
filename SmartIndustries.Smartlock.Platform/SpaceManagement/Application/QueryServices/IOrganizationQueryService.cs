using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Queries;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.QueryServices;

public interface IOrganizationQueryService
{
    Task<IEnumerable<Organization>> Handle(GetOrganizationsByUserIdQuery query, CancellationToken cancellationToken = default);
}
