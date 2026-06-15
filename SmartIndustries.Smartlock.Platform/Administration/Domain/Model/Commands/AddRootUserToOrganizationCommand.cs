namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;

public record AddRootUserToOrganizationCommand(long CreatorUserId, long RoleId);
