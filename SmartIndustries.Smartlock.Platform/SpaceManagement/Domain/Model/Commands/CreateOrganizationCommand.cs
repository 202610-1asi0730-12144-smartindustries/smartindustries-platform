namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;

public record CreateOrganizationCommand(string Name, string Description, long CreatorUserId);
