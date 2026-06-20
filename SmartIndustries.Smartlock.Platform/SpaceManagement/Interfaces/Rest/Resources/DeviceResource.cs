namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;

public record DeviceResource(long Id, long SiteId, string Name, string Status, string Mode);
