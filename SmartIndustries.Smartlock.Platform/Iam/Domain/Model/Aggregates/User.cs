using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

namespace SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Aggregates;

public partial class User
{
    public long Id { get; private set; }
    public FullName Name { get; private set; }
    public Password Password { get; private set; }
    public Email Email { get; private set; }

    public User(string firstName, string lastName, string password, string email)
    {
        Name = new FullName(firstName, lastName);
        Password = new Password(password);
        Email = new Email(email);
    }

    private User() { }
}
