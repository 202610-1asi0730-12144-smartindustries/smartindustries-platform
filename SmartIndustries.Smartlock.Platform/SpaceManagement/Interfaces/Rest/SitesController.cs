using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartIndustries.Smartlock.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SmartIndustries.Smartlock.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SmartIndustries.Smartlock.Platform.SpaceManagement.Application.CommandServices;
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
}
