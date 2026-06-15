namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model;

public enum SpaceManagementError
{
    None,
    OrganizationNotFound,
    OrganizationAlreadyExists,
    InvalidData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
