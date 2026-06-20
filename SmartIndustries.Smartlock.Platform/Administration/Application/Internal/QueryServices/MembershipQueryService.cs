using SmartIndustries.Smartlock.Platform.Administration.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Queries;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.Administration.Application.Internal.QueryServices;

public class MembershipQueryService(IMembershipRepository membershipRepository) : IMembershipQueryService
{
    public async Task<IEnumerable<Membership>> Handle(GetUsersByOrganizationIdQuery query, CancellationToken cancellationToken = default)
        => await membershipRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
}
