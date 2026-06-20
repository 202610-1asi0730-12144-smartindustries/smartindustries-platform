namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;

public record UpdateRoleInformationCommand(long RoleId, string Name, bool CanCreateSites, bool CanCreatePeople, bool CanConnectDevices);
