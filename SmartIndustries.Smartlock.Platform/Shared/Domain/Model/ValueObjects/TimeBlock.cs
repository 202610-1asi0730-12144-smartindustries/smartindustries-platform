namespace SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

public record TimeBlock
{
    public TimeOnly? Start { get; }
    public TimeOnly? End { get; }

    public TimeBlock(TimeOnly? start, TimeOnly? end)
    {
        if ((start == null) != (end == null))
            throw new ArgumentException("Both start and end must be null, or both must be non-null.");

        Start = start;
        End = end;
    }
}
