using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Queries;

namespace SmartIndustries.Smartlock.Platform.Administration.Application.QueryServices;

public interface IMembershipQueryService
{
    Task<IEnumerable<Membership>> Handle(GetUsersByOrganizationIdQuery query, CancellationToken cancellationToken = default);
}
