using SmartIndustries.Smartlock.Platform.Access.Domain.Model.ValueObjects;

namespace SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;

public partial class PersonAccess
{
    public long Id { get; private set; }
    public long PersonId { get; private set; }
    public long AccessGroupId { get; private set; }
    public PersonAccessStatus Status { get; private set; }

    public PersonAccess(long personId, long accessGroupId)
    {
        PersonId = personId;
        AccessGroupId = accessGroupId;
        Status = PersonAccessStatus.Enabled;
    }

    private PersonAccess() { }
}
