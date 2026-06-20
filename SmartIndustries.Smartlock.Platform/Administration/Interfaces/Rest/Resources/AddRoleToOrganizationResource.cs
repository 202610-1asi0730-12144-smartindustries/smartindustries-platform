namespace SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Resources;

public record AddRoleToOrganizationResource(string Name, bool CanCreateSites, bool CanCreatePeople, bool CanConnectDevices);
