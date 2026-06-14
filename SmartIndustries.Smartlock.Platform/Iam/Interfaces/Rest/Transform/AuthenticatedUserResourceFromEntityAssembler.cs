using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Iam.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.Iam.Interfaces.Rest.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
        => new(user.Id, user.Name.FirstName, user.Name.LastName, user.Email.Value, token);
}
