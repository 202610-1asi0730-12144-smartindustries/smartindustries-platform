using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;

public static class ConnectDeviceToSiteCommandFromResourceAssembler
{
    public static ConnectDeviceToSiteCommand ToCommandFromResource(ConnectDeviceToSiteResource resource, long siteId)
        => new(siteId, resource.Name, resource.Mode);
}
