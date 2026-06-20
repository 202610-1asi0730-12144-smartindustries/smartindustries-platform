namespace SmartIndustries.Smartlock.Platform.Access.Domain.Model;

public enum AccessError
{
    None,
    AccessGroupNotFound,
    PersonAccessNotFound,
    InvalidData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
