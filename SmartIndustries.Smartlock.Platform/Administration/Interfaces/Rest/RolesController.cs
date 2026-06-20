using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartIndustries.Smartlock.Platform.Administration.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Administration.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.Administration.Domain.Model.Queries;
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
[SwaggerTag("Role endpoints")]
public class RolesController(
    IRoleCommandService roleCommandService,
    IRoleQueryService roleQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get roles by organization", Description = "Returns all roles belonging to an organization")]
    [SwaggerResponse(StatusCodes.Status200OK, "Roles retrieved", typeof(IEnumerable<RoleResource>))]
    public async Task<IActionResult> GetRolesByOrganization(
        long organizationId,
        CancellationToken cancellationToken)
    {
        var query = new GetRolesByOrganizationIdQuery(organizationId);
        var roles = await roleQueryService.Handle(query, cancellationToken);
        var resources = roles.Select(RoleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Add role to organization", Description = "Create a new custom role for an organization")]
    [SwaggerResponse(StatusCodes.Status201Created, "Role created", typeof(RoleResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    public async Task<IActionResult> AddRoleToOrganization(
        long organizationId,
        [FromBody] AddRoleToOrganizationResource resource,
        CancellationToken cancellationToken)
    {
        var command = AddRoleToOrganizationCommandFromResourceAssembler.ToCommandFromResource(resource, organizationId);
        var result = await roleCommandService.Handle(command, cancellationToken);

        return AdministrationActionResultAssembler.ToActionResultFromAddRoleResult(
            this, result, problemDetailsFactory,
            role => Created($"/api/v1/organizations/{role.OrganizationId}/roles/{role.Id}",
                RoleResourceFromEntityAssembler.ToResourceFromEntity(role)));
    }

    [HttpPut("{roleId:long}")]
    [SwaggerOperation(Summary = "Update role", Description = "Update an existing role")]
    [SwaggerResponse(StatusCodes.Status200OK, "Role updated", typeof(RoleResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Role not found")]
    public async Task<IActionResult> UpdateRole(
        long roleId,
        [FromBody] UpdateRoleInformationResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateRoleInformationCommandFromResourceAssembler.ToCommandFromResource(resource, roleId);
        var result = await roleCommandService.Handle(command, cancellationToken);

        return AdministrationActionResultAssembler.ToActionResultFromAddRoleResult(
            this, result, problemDetailsFactory,
            role => Ok(RoleResourceFromEntityAssembler.ToResourceFromEntity(role)));
    }
}
