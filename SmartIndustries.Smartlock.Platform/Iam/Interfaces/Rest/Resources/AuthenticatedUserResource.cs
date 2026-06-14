namespace SmartIndustries.Smartlock.Platform.Iam.Interfaces.Rest.Resources;

public record AuthenticatedUserResource(long Id, string FirstName, string LastName, string Email, string Token);
