using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.Access.Domain.Repositories;

public interface IAccessGroupRepository : IBaseRepository<AccessGroup>
{
    Task<IEnumerable<AccessGroup>> FindByOrganizationIdAsync(long organizationId, CancellationToken cancellationToken = default);
}
