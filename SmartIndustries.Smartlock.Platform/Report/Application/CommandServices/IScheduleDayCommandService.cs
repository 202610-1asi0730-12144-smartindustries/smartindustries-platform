using SmartIndustries.Smartlock.Platform.Report.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Report.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;

namespace SmartIndustries.Smartlock.Platform.Report.Application.CommandServices;

public interface IScheduleDayCommandService
{
    Task<Result<ScheduleDay>> Handle(CreateScheduleDayCommand command, CancellationToken cancellationToken = default);
}
