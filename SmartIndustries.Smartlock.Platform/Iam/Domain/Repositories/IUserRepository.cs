using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.Iam.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> FindByEmailAsync(Email email, CancellationToken cancellationToken = default);
}