using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartIndustries.Smartlock.Platform.Administration.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Resources;
using SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Transform;
using SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SmartIndustries.Smartlock.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/organizations/{organizationId:long}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("User role endpoints")]
public class UsersController(
    IMembershipCommandService membershipCommandService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpPut("{userId:long}/role")]
    [SwaggerOperation(Summary = "Update user role in organization", Description = "Change a user's role within an organization")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role or membership not found")]
    public async Task<IActionResult> UpdateUserRole(
        long organizationId,
        long userId,
        [FromBody] UpdateUserRoleInOrganizationResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateUserRoleInOrganizationCommandFromResourceAssembler.ToCommandFromResource(resource, userId, organizationId);
        var result = await membershipCommandService.Handle(command, cancellationToken);

        return AdministrationActionResultAssembler.ToActionResultFromUpdateUserRoleResult(
            this, result, problemDetailsFactory,
            membership => Ok(new { message = "User role updated successfully" }));
    }
}
