using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

namespace SmartIndustries.Smartlock.Platform.Report.Domain.Model.Aggregates;

public partial class ScheduleDay
{
    public long Id { get; private set; }
    public long PersonId { get; private set; }
    public Day Day { get; private set; }
    public TimeBlock TimeBlock { get; private set; }

    public ScheduleDay(long personId, Day day, TimeBlock timeBlock)
    {
        PersonId = personId;
        Day = day;
        TimeBlock = timeBlock;
    }

    private ScheduleDay() { }
}
