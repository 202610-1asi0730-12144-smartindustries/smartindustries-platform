using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Repositories;

public interface IMembershipRepository : IBaseRepository<Membership>
{
    Task<Membership?> FindByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Membership>> FindByOrganizationIdAsync(long organizationId, CancellationToken cancellationToken = default);
}
