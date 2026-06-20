using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;

public partial class Person
{
    public long Id { get; private set; }
    public long OrganizationId { get; private set; }
    public FullName Name { get; private set; }
    public IdentityDocument IdentityDocument { get; private set; }
    public long? AccessGroupId { get; private set; }

    public Person(long organizationId, string firstName, string lastName, string identityDocument)
    {
        OrganizationId = organizationId;
        Name = new FullName(firstName, lastName);
        IdentityDocument = new IdentityDocument(identityDocument);
    }

    private Person() { }
}
