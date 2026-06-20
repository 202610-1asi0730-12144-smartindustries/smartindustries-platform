namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;

public record AddPersonToOrganizationCommand(long OrganizationId, string FirstName, string LastName, string IdentityDocument);
