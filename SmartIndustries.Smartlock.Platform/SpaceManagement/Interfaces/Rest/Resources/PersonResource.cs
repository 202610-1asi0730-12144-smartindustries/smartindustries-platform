namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;

public record PersonResource(long Id, long OrganizationId, string FirstName, string LastName, string IdentityDocument);
