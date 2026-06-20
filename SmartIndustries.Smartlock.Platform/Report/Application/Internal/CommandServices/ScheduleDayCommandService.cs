using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartIndustries.Smartlock.Platform.Report.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Report.Domain.Model;
using SmartIndustries.Smartlock.Platform.Report.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Report.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Report.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Resources.Errors;

namespace SmartIndustries.Smartlock.Platform.Report.Application.Internal.CommandServices;

public class ScheduleDayCommandService(
    IScheduleDayRepository scheduleDayRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer) : IScheduleDayCommandService
{
    public async Task<Result<ScheduleDay>> Handle(CreateScheduleDayCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var scheduleDay = new ScheduleDay(command.PersonId, command.Day, command.TimeBlock);
            await scheduleDayRepository.AddAsync(scheduleDay, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<ScheduleDay>.Success(scheduleDay);
        }
        catch (ArgumentException exception)
        {
            return Result<ScheduleDay>.Failure(ReportError.InvalidData, exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<ScheduleDay>.Failure(ReportError.OperationCancelled, localizer["ReportError.OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<ScheduleDay>.Failure(ReportError.DatabaseError, localizer["ReportError.DatabaseError"]);
        }
        catch (Exception)
        {
            return Result<ScheduleDay>.Failure(ReportError.InternalServerError, localizer["ReportError.InternalServerError"]);
        }
    }
}
