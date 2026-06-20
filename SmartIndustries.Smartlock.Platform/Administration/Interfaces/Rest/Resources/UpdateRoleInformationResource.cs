namespace SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Resources;

public record UpdateRoleInformationResource(string Name, bool CanCreateSites, bool CanCreatePeople, bool CanConnectDevices);
