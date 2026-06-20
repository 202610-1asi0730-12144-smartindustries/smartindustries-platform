using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Access.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Configuration;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Repositories;

namespace SmartIndustries.Smartlock.Platform.Access.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AccessGroupRepository(AppDbContext context) : BaseRepository<AccessGroup>(context), IAccessGroupRepository
{
    public async Task<IEnumerable<AccessGroup>> FindByOrganizationIdAsync(long organizationId, CancellationToken cancellationToken = default)
        => await context.Set<AccessGroup>().Where(accessGroup => accessGroup.OrganizationId == organizationId).ToListAsync(cancellationToken);
}
