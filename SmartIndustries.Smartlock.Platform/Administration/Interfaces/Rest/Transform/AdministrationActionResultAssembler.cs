using Microsoft.AspNetCore.Mvc;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Interfaces.Rest.ProblemDetails;

namespace SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Transform;

public static class AdministrationActionResultAssembler
{
    private static int ToStatusCode(AdministrationError error)
    {
        return error switch
        {
            AdministrationError.InvalidData => StatusCodes.Status400BadRequest,
            AdministrationError.OperationCancelled => StatusCodes.Status409Conflict,
            AdministrationError.DatabaseError => StatusCodes.Status500InternalServerError,
            AdministrationError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromAddRoleResult(
        ControllerBase controller,
        Result<Role> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Role, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCode((AdministrationError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }

    public static IActionResult ToActionResultFromUpdateUserRoleResult(
        ControllerBase controller,
        Result<Membership> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Membership, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCode((AdministrationError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
}
