using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Configuration;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Repositories;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class OrganizationRepository(AppDbContext context) : BaseRepository<Organization>(context), IOrganizationRepository
{
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
        => await Context.Set<Organization>().AnyAsync(o => o.Name.Value == name, ct);

    public async Task<IEnumerable<Organization>> FindByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        => await Context.Set<Organization>()
            .Join(Context.Set<Role>(),
                organization => organization.Id,
                role => role.OrganizationId,
                (organization, role) => new { organization, role })
            .Join(Context.Set<Membership>(),
                joined => joined.role.Id,
                membership => membership.RoleId,
                (joined, membership) => new { joined.organization, joined.role, membership })
            .Where(joined => joined.membership.UserId == userId)
            .Select(joined => joined.organization)
            .Distinct()
            .ToListAsync(cancellationToken);
}
