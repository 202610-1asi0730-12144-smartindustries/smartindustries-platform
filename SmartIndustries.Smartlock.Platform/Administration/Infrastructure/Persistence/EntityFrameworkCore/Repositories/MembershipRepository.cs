using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Configuration;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Repositories;

namespace SmartIndustries.Smartlock.Platform.Administration.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MembershipRepository(AppDbContext context) : BaseRepository<Membership>(context), IMembershipRepository
{
    public async Task<Membership?> FindByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        => await context.Set<Membership>().FirstOrDefaultAsync(membership => membership.UserId == userId, cancellationToken);
}
