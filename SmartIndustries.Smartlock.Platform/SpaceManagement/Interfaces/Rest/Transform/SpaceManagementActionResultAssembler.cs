using Microsoft.AspNetCore.Mvc;
using SmartIndustries.Smartlock.Platform.Shared.Application.Model;
using SmartIndustries.Smartlock.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Aggregates;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;

public static class SpaceManagementActionResultAssembler
{
    private static int ToStatusCode(SpaceManagementError error)
    {
        return error switch
        {
            SpaceManagementError.OrganizationAlreadyExists => StatusCodes.Status409Conflict,
            SpaceManagementError.InvalidData => StatusCodes.Status400BadRequest,
            SpaceManagementError.OperationCancelled => StatusCodes.Status409Conflict,
            SpaceManagementError.DatabaseError => StatusCodes.Status500InternalServerError,
            SpaceManagementError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }

    public static IActionResult ToActionResultFromCreateOrganizationResult(
        ControllerBase controller,
        Result<Organization> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Organization, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCode((SpaceManagementError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
}
