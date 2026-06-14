using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.Iam.Interfaces.Rest.Resources;

namespace SmartIndustries.Smartlock.Platform.Iam.Interfaces.Rest.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
        => new(resource.FirstName, resource.LastName, resource.Email, resource.Password);
}
