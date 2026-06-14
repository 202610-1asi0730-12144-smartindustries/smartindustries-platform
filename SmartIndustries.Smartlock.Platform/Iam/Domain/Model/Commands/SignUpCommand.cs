namespace SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Commands;

public record SignUpCommand(string FirstName, string LastName, string Email, string Password);
