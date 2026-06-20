namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;

public record AddSiteToOrganizationCommand(long OrganizationId, string Name, string Description);
