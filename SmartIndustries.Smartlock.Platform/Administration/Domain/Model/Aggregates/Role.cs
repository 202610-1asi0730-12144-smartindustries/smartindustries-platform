using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.ValueObjects;
using SmartIndustries.Smartlock.Platform.Shared.Domain.Model.ValueObjects;

namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;

public partial class Role
{
    public long Id { get; private set; }
    public long OrganizationId { get; private set; }
    public GenericName Name { get; private set; }
    public RolePermissions Permissions { get; private set; }
    public bool Deletable { get; private set; }

    private Role() { }

    public static Role CreateRoot(long organizationId)
        => new()
        {
            OrganizationId = organizationId,
            Name = new GenericName("Root"),
            Permissions = new RolePermissions(true, true, true),
            Deletable = false
        };

    public static Role CreateBasic(long organizationId)
        => new()
        {
            OrganizationId = organizationId,
            Name = new GenericName("Basic"),
            Permissions = new RolePermissions(false, false, false),
            Deletable = true
        };
}
