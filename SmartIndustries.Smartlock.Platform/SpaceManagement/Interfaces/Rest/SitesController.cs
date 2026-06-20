using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SmartIndustries.Smartlock.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.CommandServices;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Domain.Model.Commands;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Resources;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartIndustries.Smartlock.Platform.SpaceManagement.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/organizations/{organizationId:long}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Site endpoints")]
public class SitesController(
    ISiteCommandService siteCommandService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Add site to organization", Description = "Add a new site to an organization")]
    [SwaggerResponse(StatusCodes.Status201Created, "Site created", typeof(SiteResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    public async Task<IActionResult> AddSiteToOrganization(
        long organizationId,
        [FromBody] AddSiteToOrganizationResource resource,
        CancellationToken cancellationToken)
    {
        var command = AddSiteToOrganizationCommandFromResourceAssembler.ToCommandFromResource(resource, organizationId);
        var result = await siteCommandService.Handle(command, cancellationToken);

        return SpaceManagementActionResultAssembler.ToActionResultFromAddSiteResult(
            this, result, problemDetailsFactory,
            site => Created($"/api/v1/organizations/{site.OrganizationId}/sites/{site.Id}",
                SiteResourceFromEntityAssembler.ToResourceFromEntity(site)));
    }

    [HttpPut("{siteId:long}")]
    [SwaggerOperation(Summary = "Update site", Description = "Update an existing site")]
    [SwaggerResponse(StatusCodes.Status200OK, "Site updated", typeof(SiteResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Site not found")]
    public async Task<IActionResult> UpdateSite(
        long siteId,
        [FromBody] UpdateSiteInformationResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateSiteInformationCommandFromResourceAssembler.ToCommandFromResource(resource, siteId);
        var result = await siteCommandService.Handle(command, cancellationToken);

        return SpaceManagementActionResultAssembler.ToActionResultFromAddSiteResult(
            this, result, problemDetailsFactory,
            site => Ok(SiteResourceFromEntityAssembler.ToResourceFromEntity(site)));
    }

    [HttpDelete("{siteId:long}")]
    [SwaggerOperation(Summary = "Delete site", Description = "Delete an existing site and all its devices")]
    [SwaggerResponse(StatusCodes.Status200OK, "Site deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Site not found")]
    public async Task<IActionResult> DeleteSite(
        long siteId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteSiteCommand(siteId);
        var result = await siteCommandService.Handle(command, cancellationToken);

        return SpaceManagementActionResultAssembler.ToActionResultFromAddSiteResult(
            this, result, problemDetailsFactory,
            site => Ok(new { message = "Site deleted successfully" }));
    }
}
