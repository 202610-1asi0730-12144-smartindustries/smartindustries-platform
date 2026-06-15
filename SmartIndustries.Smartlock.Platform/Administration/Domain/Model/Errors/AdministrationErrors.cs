using SmartIndustries.Smartlock.Platform.Shared.Domain.Model;

namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Errors;

public static class AdministrationErrors
{
    public static readonly Error RoleNotFound = new("Administration.RoleNotFound", "Role not found.");
}
