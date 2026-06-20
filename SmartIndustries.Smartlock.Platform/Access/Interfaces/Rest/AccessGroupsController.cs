using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartIndustries.Smartlock.Platform.Access.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.Access.Application.QueryServices;
using SmartIndustries.Smartlock.Platform.Access.Domain.Model.Queries;
using SmartIndustries.Smartlock.Platform.Access.Interfaces.Rest.Resources;
using SmartIndustries.Smartlock.Platform.Access.Interfaces.Rest.Transform;
using SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SmartIndustries.Smartlock.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartIndustries.Smartlock.Platform.Access.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/organizations/{organizationId:long}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Access group endpoints")]
public class AccessGroupsController(
    IAccessGroupCommandService accessGroupCommandService,
    IAccessGroupQueryService accessGroupQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get access groups by organization", Description = "Returns all access groups belonging to an organization")]
    [SwaggerResponse(StatusCodes.Status200OK, "Access groups retrieved", typeof(IEnumerable<AccessGroupResource>))]
    public async Task<IActionResult> GetAccessGroupsByOrganization(
        long organizationId,
        CancellationToken cancellationToken)
    {
        var query = new GetAccessGroupsByOrganizationIdQuery(organizationId);
        var accessGroups = await accessGroupQueryService.Handle(query, cancellationToken);
        var resources = accessGroups.Select(AccessGroupResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create access group", Description = "Create a new access group for an organization")]
    [SwaggerResponse(StatusCodes.Status201Created, "Access group created", typeof(AccessGroupResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    public async Task<IActionResult> CreateAccessGroup(
        long organizationId,
        [FromBody] CreateAccessGroupResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateAccessGroupCommandFromResourceAssembler.ToCommandFromResource(resource, organizationId);
        var result = await accessGroupCommandService.Handle(command, cancellationToken);

        return AccessActionResultAssembler.ToActionResultFromCreateAccessGroupResult(
            this, result, problemDetailsFactory,
            accessGroup => Created($"/api/v1/organizations/{accessGroup.OrganizationId}/access-groups/{accessGroup.Id}",
                AccessGroupResourceFromEntityAssembler.ToResourceFromEntity(accessGroup)));
    }
}
