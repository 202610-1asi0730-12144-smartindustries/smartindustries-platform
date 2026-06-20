using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Queries;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.Internal.QueryServices;

public class SiteQueryService(ISiteRepository siteRepository) : ISiteQueryService
{
    public async Task<IEnumerable<Site>> Handle(GetSitesByOrganizationIdQuery query, CancellationToken cancellationToken = default)
        => await siteRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
}
