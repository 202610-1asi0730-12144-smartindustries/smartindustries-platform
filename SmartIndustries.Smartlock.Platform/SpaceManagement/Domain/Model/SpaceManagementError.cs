namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model;

public enum SpaceManagementError
{
    None,
    OrganizationNotFound,
    OrganizationAlreadyExists,
    SiteNotFound,
    DeviceNotFound,
    PersonNotFound,
    InvalidData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
