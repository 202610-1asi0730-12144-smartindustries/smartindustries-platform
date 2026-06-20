using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Queries;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.Internal.QueryServices;

public class OrganizationQueryService(IOrganizationRepository organizationRepository) : IOrganizationQueryService
{
    public async Task<IEnumerable<Organization>> Handle(GetOrganizationsByUserIdQuery query, CancellationToken cancellationToken = default)
        => await organizationRepository.FindByUserIdAsync(query.UserId, cancellationToken);
}
