using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Queries;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.QueryServices;

public interface IDeviceQueryService
{
    Task<IEnumerable<Device>> Handle(GetDevicesByOrganizationIdQuery query, CancellationToken cancellationToken = default);
}
