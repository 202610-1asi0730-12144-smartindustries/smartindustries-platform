using Microsoft.AspNetCore.Mvc;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Model;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Interfaces.Rest.ProblemDetails;

namespace SmartIndustries.Smartlock.Platform.Iam.Interfaces.Rest.Transform;

public static class IamActionResultAssembler
{
    private static int ToStatusCode(IamError error)
    {
        return error switch
        {
            IamError.InvalidCredentials => StatusCodes.Status400BadRequest,
            IamError.EmailAlreadyTaken => StatusCodes.Status409Conflict,
            IamError.OperationCancelled => StatusCodes.Status409Conflict,
            IamError.DatabaseError => StatusCodes.Status500InternalServerError,
            IamError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }
    public static IActionResult ToActionResultFromSignUpResult(
        ControllerBase controller,
        Result result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction();

        var statusCode = ToStatusCode((IamError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
    
}