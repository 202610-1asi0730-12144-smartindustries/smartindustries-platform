using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartIndustries.Smartlock.Platform.Administration.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Administration.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Queries;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Resources;
using SmartIndustries.Smartlock.Platform.Administration.Interfaces.Rest.Transform;
using SmartIndustries.Smartlock.Platform.Iam.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.Iam.Domain.Model.queries;
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
    IMembershipQueryService membershipQueryService,
    IUserQueryService userQueryService,
    IRoleRepository roleRepository,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get users by organization", Description = "Returns all users belonging to an organization with their roles")]
    [SwaggerResponse(StatusCodes.Status200OK, "Users retrieved", typeof(IEnumerable<UserWithRoleResource>))]
    public async Task<IActionResult> GetUsersByOrganization(
        long organizationId,
        CancellationToken cancellationToken)
    {
        var query = new GetUsersByOrganizationIdQuery(organizationId);
        var memberships = await membershipQueryService.Handle(query, cancellationToken);

        var resources = new List<UserWithRoleResource>();
        foreach (var membership in memberships)
        {
            var user = await userQueryService.Handle(new GetUserByIdQuery(membership.UserId), cancellationToken);
            var role = await roleRepository.FindByIdAsync(membership.RoleId, cancellationToken);
            resources.Add(new UserWithRoleResource(
                membership.UserId,
                user?.Email.Value ?? string.Empty,
                user?.Name.FirstName ?? string.Empty,
                user?.Name.LastName ?? string.Empty,
                membership.RoleId,
                role?.Name.Value ?? string.Empty));
        }

        return Ok(resources);
    }

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
