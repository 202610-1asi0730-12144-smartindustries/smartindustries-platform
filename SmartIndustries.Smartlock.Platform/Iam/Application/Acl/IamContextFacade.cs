using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Iam.Interfaces.Acl;

namespace SmartIndustries.Smartlock.Platform.Iam.Application.Acl;

public class IamContextFacade : IIamContextFacade
{
    public long? GetCurrentUserId(HttpContext httpContext)
    {
        var user = httpContext.Items["User"] as User;
        return user?.Id;
    }
}
