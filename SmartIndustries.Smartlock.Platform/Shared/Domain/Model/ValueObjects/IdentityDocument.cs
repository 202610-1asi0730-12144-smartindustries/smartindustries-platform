using System.Text.RegularExpressions;

namespace SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

public record IdentityDocument
{
    public string Value { get; }

    private static readonly Regex Pattern = new(@"^[0-9]{8}$", RegexOptions.Compiled);

    public IdentityDocument(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Identity document must not be null or blank.", nameof(value));
        if (!Pattern.IsMatch(value))
            throw new ArgumentException("Identity document must be exactly 8 digits.", nameof(value));

        Value = value;
    }
}
