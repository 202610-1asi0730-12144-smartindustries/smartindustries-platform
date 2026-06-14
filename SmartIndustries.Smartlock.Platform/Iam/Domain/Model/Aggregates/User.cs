using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

namespace SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Aggregates;

public partial class User
{
    public long Id { get; private set; }
    public FullName Name { get; private set; }
    public string PasswordHash { get; private set; }
    public Email Email { get; private set; }

    public User(string firstName, string lastName, string passwordHash, string email)
    {
        Name = new FullName(firstName, lastName);
        PasswordHash = passwordHash;
        Email = new Email(email);
    }

    private User() { }
}
