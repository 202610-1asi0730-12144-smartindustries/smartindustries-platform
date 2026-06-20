using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SmartIndustries.Smartlock.Platform.Iam.Interfaces.Acl;
using SmartIndustries.Smartlock.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Organization endpoints")]
public class OrganizationsController(
    IOrganizationCommandService organizationCommandService,
    IIamContextFacade iamContextFacade,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Create organization", Description = "Create a new organization")]
    [SwaggerResponse(StatusCodes.Status201Created, "Organization created", typeof(OrganizationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Organization already exists")]
    public async Task<IActionResult> CreateOrganization(
        [FromBody] CreateOrganizationResource resource,
        CancellationToken cancellationToken)
    {
        var creatorUserId = iamContextFacade.GetCurrentUserId(HttpContext);
        if (creatorUserId == null)
            return Unauthorized();

        var command = CreateOrganizationCommandFromResourceAssembler.ToCommandFromResource(resource, creatorUserId.Value);
        var result = await organizationCommandService.Handle(command, cancellationToken);

        return SpaceManagementActionResultAssembler.ToActionResultFromCreateOrganizationResult(
            this, result, problemDetailsFactory,
            organization => Created($"/api/v1/organizations/{organization.Id}",
                OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization)));
    }

    [HttpPut("{organizationId:long}")]
    [SwaggerOperation(Summary = "Update organization", Description = "Update an existing organization")]
    [SwaggerResponse(StatusCodes.Status200OK, "Organization updated", typeof(OrganizationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Organization not found")]
    public async Task<IActionResult> UpdateOrganization(
        long organizationId,
        [FromBody] UpdateOrganizationInformationResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateOrganizationInformationCommandFromResourceAssembler.ToCommandFromResource(resource, organizationId);
        var result = await organizationCommandService.Handle(command, cancellationToken);

        return SpaceManagementActionResultAssembler.ToActionResultFromCreateOrganizationResult(
            this, result, problemDetailsFactory,
            organization => Ok(OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization)));
    }

    [HttpDelete("{organizationId:long}")]
    [SwaggerOperation(Summary = "Delete organization", Description = "Delete an organization and all related data")]
    [SwaggerResponse(StatusCodes.Status200OK, "Organization deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Organization not found")]
    public async Task<IActionResult> DeleteOrganization(
        long organizationId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteOrganizationCommand(organizationId);
        var result = await organizationCommandService.Handle(command, cancellationToken);

        return SpaceManagementActionResultAssembler.ToActionResultFromCreateOrganizationResult(
            this, result, problemDetailsFactory,
            organization => Ok(new { message = "Organization deleted successfully" }));
    }
}
