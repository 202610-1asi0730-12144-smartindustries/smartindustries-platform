namespace SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

public record Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password is required.", nameof(value));
        if (value.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters.", nameof(value));
        if (value.Length > 128)
            throw new ArgumentException("Password must be at most 128 characters.", nameof(value));

        Value = value;
    }
}
