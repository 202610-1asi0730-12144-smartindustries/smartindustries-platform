using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Iam.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.Iam.Interfaces.Rest.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
        => new(resource.Email, resource.Password);
}
