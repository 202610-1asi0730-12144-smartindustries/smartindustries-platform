namespace SmartIndustries.Smartlock.Platform.Administration.Domain.Model.ValueObjects;

public record RolePermissions
{
    public bool CanCreateSites { get; }
    public bool CanCreatePeople { get; }
    public bool CanConnectDevices { get; }

    public RolePermissions(bool canCreateSites, bool canCreatePeople, bool canConnectDevices)
    {
        CanCreateSites = canCreateSites;
        CanCreatePeople = canCreatePeople;
        CanConnectDevices = canConnectDevices;
    }
}
