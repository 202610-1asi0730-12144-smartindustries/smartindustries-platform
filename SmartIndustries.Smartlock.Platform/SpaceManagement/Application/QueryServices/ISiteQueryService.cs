using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Queries;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.QueryServices;

public interface ISiteQueryService
{
    Task<IEnumerable<Site>> Handle(GetSitesByOrganizationIdQuery query, CancellationToken cancellationToken = default);
}
