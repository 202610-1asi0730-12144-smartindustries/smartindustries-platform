namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Commands;

public record CreateRootRoleCommand(long OrganizationId, long CreatorUserId);
