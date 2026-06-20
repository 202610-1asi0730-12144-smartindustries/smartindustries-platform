using Microsoft.AspNetCore.Mvc;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Interfaces.Rest.ProblemDetails;

namespace SmartIndustries.Smartlock.Platform.Access.Interfaces.Rest.Transform;

public static class AccessActionResultAssembler
{
    private static int ToStatusCode(AccessError error)
    {
        return error switch
        {
            AccessError.InvalidData => StatusCodes.Status400BadRequest,
            AccessError.OperationCancelled => StatusCodes.Status409Conflict,
            AccessError.DatabaseError => StatusCodes.Status500InternalServerError,
            AccessError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateAccessGroupResult(
        ControllerBase controller,
        Result<AccessGroup> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<AccessGroup, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCode((AccessError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
}
