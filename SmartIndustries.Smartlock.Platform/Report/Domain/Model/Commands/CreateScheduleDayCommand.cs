using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

namespace SmartIndustries.Smartlock.Platform.Report.Domain.Model.Commands;

public record CreateScheduleDayCommand(long PersonId, Day Day, TimeBlock TimeBlock);
