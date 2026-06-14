namespace SmartIndustries.Smartlock.Platform.Iam.Domain.Model;

public enum IamError
{
    None,
    UserNotFound,
    EmailAlreadyTaken,
    InvalidCredentials,
    InvalidData,
    OperationCancelled,
    DatabaseError,
    InternalServerError,
    ExternalServiceError
}
