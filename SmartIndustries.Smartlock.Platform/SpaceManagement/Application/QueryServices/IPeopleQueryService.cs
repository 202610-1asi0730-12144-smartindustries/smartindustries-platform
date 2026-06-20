using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Queries;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.QueryServices;

public interface IPeopleQueryService
{
    Task<IEnumerable<Person>> Handle(GetPeopleByOrganizationIdQuery query, CancellationToken cancellationToken = default);
}
