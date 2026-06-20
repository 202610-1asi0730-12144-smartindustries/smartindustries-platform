namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;

public record UpdateDeviceInformationCommand(long DeviceId, long SiteId, string Name, string Mode);
