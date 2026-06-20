namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;

public record UpdatePersonInformationCommand(long PersonId, string FirstName, string LastName, string IdentityDocument);
