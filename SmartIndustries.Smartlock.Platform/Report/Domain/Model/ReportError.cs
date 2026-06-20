namespace SmartIndustries.Smartlock.Platform.Report.Domain.Model;

public enum ReportError
{
    None,
    ScheduleDayNotFound,
    InvalidData,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
