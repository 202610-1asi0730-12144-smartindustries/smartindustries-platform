namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;

public record ConnectDeviceToSiteCommand(long SiteId, string Name, string Mode);
