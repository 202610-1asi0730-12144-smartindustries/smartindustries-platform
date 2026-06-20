using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

public interface ISiteRepository : IBaseRepository<Site>
{
    Task<IEnumerable<Site>> FindByOrganizationIdAsync(long organizationId, CancellationToken cancellationToken = default);
}
