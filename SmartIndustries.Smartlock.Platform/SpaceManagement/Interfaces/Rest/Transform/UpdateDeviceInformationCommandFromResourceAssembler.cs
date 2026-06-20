using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;

public static class UpdateDeviceInformationCommandFromResourceAssembler
{
    public static UpdateDeviceInformationCommand ToCommandFromResource(UpdateDeviceInformationResource resource, long deviceId)
        => new(deviceId, resource.SiteId, resource.Name, resource.Mode);
}
