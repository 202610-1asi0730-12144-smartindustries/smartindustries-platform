using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<IEnumerable<Role>> FindByOrganizationIdAsync(long organizationId, CancellationToken cancellationToken = default);
}
