using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Configuration;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Repositories;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SiteRepository(AppDbContext context) : BaseRepository<Site>(context), ISiteRepository
{
    public async Task<IEnumerable<Site>> FindByOrganizationIdAsync(long organizationId, CancellationToken cancellationToken = default)
        => await context.Set<Site>().Where(site => site.OrganizationId == organizationId).ToListAsync(cancellationToken);
}
