namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Model;

public enum AdministrationError
{
    None,
    RoleNotFound,
    MembershipNotFound,
    InvalidData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
