using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Queries;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Repositories;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Application.Internal.QueryServices;

public class DeviceQueryService(IDeviceRepository deviceRepository) : IDeviceQueryService
{
    public async Task<IEnumerable<Device>> Handle(GetDevicesByOrganizationIdQuery query, CancellationToken cancellationToken = default)
        => await deviceRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
}
