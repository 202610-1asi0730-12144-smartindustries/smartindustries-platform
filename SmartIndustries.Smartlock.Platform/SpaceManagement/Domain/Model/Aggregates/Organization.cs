using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;

public partial class Organization
{
    public long Id { get; private set; }
    public GenericName Name { get; private set; }
    public string Description { get; private set; }

    public Organization(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Organization name is required.", nameof(name));
        if (name.Length > 100)
            throw new ArgumentException("Organization name must be at most 100 characters.", nameof(name));
        if (description?.Length > 500)
            throw new ArgumentException("Description must be at most 500 characters.", nameof(description));

        Name = new GenericName(name);
        Description = description?.Trim() ?? string.Empty;
    }

    private Organization() { }

    public void UpdateInformation(string name, string description)
    {
        Name = new GenericName(name);
        Description = description?.Trim() ?? string.Empty;
    }
}
