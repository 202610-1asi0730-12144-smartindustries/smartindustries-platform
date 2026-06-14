namespace SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

public record FullName
{
    public string FirstName { get; }
    public string LastName { get; }

    public FullName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2)
            throw new ArgumentException("First name must be at least 2 characters.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2)
            throw new ArgumentException("Last name must be at least 2 characters.", nameof(lastName));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }
}
