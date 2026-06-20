using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Queries;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.Internal.QueryServices;

public class PeopleQueryService(IPersonRepository personRepository) : IPeopleQueryService
{
    public async Task<IEnumerable<Person>> Handle(GetPeopleByOrganizationIdQuery query, CancellationToken cancellationToken = default)
        => await personRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
}
