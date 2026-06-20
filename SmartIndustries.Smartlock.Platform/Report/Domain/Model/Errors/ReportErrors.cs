using SmartIndustries.Smartlock.Platform.Shared.Domain.Model;

namespace SmartIndustries.Smartlock.Platform.Report.Domain.Model.Errors;

public static class ReportErrors
{
    public static readonly Error ScheduleDayNotFound = new("Report.ScheduleDayNotFound", "Schedule day not found.");
}
