using Microsoft.EntityFrameworkCore;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Configuration;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Repositories;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class PersonRepository(AppDbContext context) : BaseRepository<Person>(context), IPersonRepository
{
    public async Task<IEnumerable<Person>> FindByOrganizationIdAsync(long organizationId, CancellationToken cancellationToken = default)
        => await context.Set<Person>().Where(person => person.OrganizationId == organizationId).ToListAsync(cancellationToken);
}
