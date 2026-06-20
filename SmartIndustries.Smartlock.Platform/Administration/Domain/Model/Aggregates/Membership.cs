namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;

public partial class Membership
{
    public long Id { get; private set; }
    public long UserId { get; private set; }
    public long RoleId { get; private set; }

    public Membership(long userId, long roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    private Membership() { }

    public void UpdateRole(long roleId)
    {
        RoleId = roleId;
    }
}
