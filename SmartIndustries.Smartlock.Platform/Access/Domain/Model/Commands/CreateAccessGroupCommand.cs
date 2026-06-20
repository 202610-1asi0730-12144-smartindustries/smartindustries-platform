namespace SmartIndustries.Smartlock.Platform.Access.Domain.Model.Commands;

public record CreateAccessGroupCommand(long OrganizationId, string Name, string Description);
