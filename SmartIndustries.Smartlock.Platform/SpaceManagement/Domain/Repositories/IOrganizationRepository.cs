using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

public interface IOrganizationRepository : IBaseRepository<Organization>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    Task<IEnumerable<Organization>> FindByUserIdAsync(long userId, CancellationToken cancellationToken = default);
}
