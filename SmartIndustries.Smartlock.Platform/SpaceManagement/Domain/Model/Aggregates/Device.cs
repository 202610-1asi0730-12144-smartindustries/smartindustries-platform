using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.ValueObjects;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;

public partial class Device
{
    public long Id { get; private set; }
    public long SiteId { get; private set; }
    public GenericName Name { get; private set; }
    public DeviceStatus Status { get; private set; }
    public DeviceMode Mode { get; private set; }

    public Device(long siteId, string name, string mode)
    {
        SiteId = siteId;
        Name = new GenericName(name);
        Mode = Enum.Parse<DeviceMode>(mode, ignoreCase: true);
        Status = DeviceStatus.Online;
    }

    private Device() { }

    public void UpdateInformation(long siteId, string name, string mode)
    {
        SiteId = siteId;
        Name = new GenericName(name);
        Mode = Enum.Parse<DeviceMode>(mode, ignoreCase: true);
    }
}
