using SmartIndustries.Smartlock.Platform.Shared.Domain.Model;

namespace SmartIndustries.Smartlock.Platform.Access.Domain.Model.Errors;

public static class AccessErrors
{
    public static readonly Error AccessGroupNotFound = new("Access.AccessGroupNotFound", "Access group not found.");
    public static readonly Error PersonAccessNotFound = new("Access.PersonAccessNotFound", "Person access not found.");
}
