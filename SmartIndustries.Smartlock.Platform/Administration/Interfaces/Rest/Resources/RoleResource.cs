namespace SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Resources;

public record RoleResource(long Id, long OrganizationId, string Name, bool CanCreateSites, bool CanCreatePeople, bool CanConnectDevices, bool Deletable);
