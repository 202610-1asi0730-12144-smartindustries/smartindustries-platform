using SmartIndustries.Smartlock.Platform.Access.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Queries;
using SmartIndustries.Smartlock.Platform.Access.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.Access.Application.Internal.QueryServices;

public class AccessGroupQueryService(IAccessGroupRepository accessGroupRepository) : IAccessGroupQueryService
{
    public async Task<IEnumerable<AccessGroup>> Handle(GetAccessGroupsByOrganizationIdQuery query, CancellationToken cancellationToken = default)
        => await accessGroupRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
}
