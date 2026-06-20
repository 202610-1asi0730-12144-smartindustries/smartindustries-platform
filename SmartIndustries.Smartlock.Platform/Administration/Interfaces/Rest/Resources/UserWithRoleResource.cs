namespace SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Resources;

public record UserWithRoleResource(long UserId, string Email, string FirstName, string LastName, long RoleId, string RoleName);
