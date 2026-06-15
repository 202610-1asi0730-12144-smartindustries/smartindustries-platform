namespace SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

public record GenericName
{
    public string Value { get; }

    public GenericName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Name is required.", nameof(value));

        Value = value.Trim();
    }
}
