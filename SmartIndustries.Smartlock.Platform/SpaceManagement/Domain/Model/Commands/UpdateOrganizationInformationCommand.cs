namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;

public record UpdateOrganizationInformationCommand(long OrganizationId, string Name, string Description);
