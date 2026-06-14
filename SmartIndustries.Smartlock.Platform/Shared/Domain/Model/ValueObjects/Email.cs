using System.Text.RegularExpressions;

namespace SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

public record Email
{
    public string Value { get; }

    private static readonly Regex Pattern = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email is required.", nameof(value));

        value = value.Trim();

        if (value.Length > 254)
            throw new ArgumentException("Email must be at most 254 characters.", nameof(value));

        if (!Pattern.IsMatch(value))
            throw new ArgumentException("Email format is invalid.", nameof(value));

        Value = value;
    }
}
