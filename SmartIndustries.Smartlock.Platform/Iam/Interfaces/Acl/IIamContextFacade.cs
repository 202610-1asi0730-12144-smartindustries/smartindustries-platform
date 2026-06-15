namespace SmartIndustries.Smartlock.Platform.Iam.Interfaces.Acl;

public interface IIamContextFacade
{
    long? GetCurrentUserId(HttpContext httpContext);
}
