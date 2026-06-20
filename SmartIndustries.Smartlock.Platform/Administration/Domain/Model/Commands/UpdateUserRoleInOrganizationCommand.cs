namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;

public record UpdateUserRoleInOrganizationCommand(long UserId, long OrganizationId, long NewRoleId);
