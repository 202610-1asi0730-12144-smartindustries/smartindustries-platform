namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model;

public enum SpaceManagementError
{
    None,
    OrganizationNotFound,
    OrganizationAlreadyExists,
    SiteNotFound,
    InvalidData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
