using SmartIndustries.Smartlock.Platform.Administration.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Queries;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.Administration.Application.Internal.QueryServices;

public class RoleQueryService(IRoleRepository roleRepository) : IRoleQueryService
{
    public async Task<IEnumerable<Role>> Handle(GetRolesByOrganizationIdQuery query, CancellationToken cancellationToken = default)
        => await roleRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
}
