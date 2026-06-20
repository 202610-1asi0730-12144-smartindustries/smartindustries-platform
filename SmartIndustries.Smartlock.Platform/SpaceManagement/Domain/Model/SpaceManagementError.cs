namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model;

public enum SpaceManagementError
{
    None,
    OrganizationNotFound,
    OrganizationAlreadyExists,
    SiteNotFound,
    DeviceNotFound,
    InvalidData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
