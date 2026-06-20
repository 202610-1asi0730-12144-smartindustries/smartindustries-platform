namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;

public record AddRoleToOrganizationCommand(long OrganizationId, string Name, bool CanCreateSites, bool CanCreatePeople, bool CanConnectDevices);
