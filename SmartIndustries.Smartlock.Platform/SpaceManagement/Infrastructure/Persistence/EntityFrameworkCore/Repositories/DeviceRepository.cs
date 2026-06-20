using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Configuration;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Repositories;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class DeviceRepository(AppDbContext context) : BaseRepository<Device>(context), IDeviceRepository
{
    public async Task<IEnumerable<Device>> FindByOrganizationIdAsync(long organizationId, CancellationToken cancellationToken = default)
        => await context.Set<Device>()
            .Join(context.Set<Site>(),
                device => device.SiteId,
                site => site.Id,
                (device, site) => new { device, site })
            .Where(joined => joined.site.OrganizationId == organizationId)
            .Select(joined => joined.device)
            .ToListAsync(cancellationToken);
}
