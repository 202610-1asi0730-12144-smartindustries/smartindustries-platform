using SmartIndustries.Smartlock.Platform.Shared.Domain.Model;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Errors;

public static class SpaceManagementErrors
{
    public static readonly Error OrganizationNotFound = new("SpaceManagement.OrganizationNotFound", "Organization not found.");
    public static readonly Error OrganizationAlreadyExists = new("SpaceManagement.OrganizationAlreadyExists", "Organization already exists.");
}
