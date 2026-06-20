using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Configuration;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Repositories;

namespace SmartIndustries.Smartlock.Platform.Administration.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RoleRepository(AppDbContext ctx) : BaseRepository<Role>(ctx), IRoleRepository
{
    public async Task<IEnumerable<Role>> FindByOrganizationIdAsync(long organizationId, CancellationToken cancellationToken = default)
        => await ctx.Set<Role>().Where(role => role.OrganizationId == organizationId).ToListAsync(cancellationToken);
}
