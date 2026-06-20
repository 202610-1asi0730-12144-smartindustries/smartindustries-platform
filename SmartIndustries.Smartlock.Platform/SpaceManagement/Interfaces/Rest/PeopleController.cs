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
[SwaggerTag("People endpoints")]
public class PeopleController(
    IPersonCommandService personCommandService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Add person to organization", Description = "Add a new person to an organization")]
    [SwaggerResponse(StatusCodes.Status201Created, "Person created", typeof(PersonResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    public async Task<IActionResult> AddPersonToOrganization(
        long organizationId,
        [FromBody] AddPersonToOrganizationResource resource,
        CancellationToken cancellationToken)
    {
        var command = AddPersonToOrganizationCommandFromResourceAssembler.ToCommandFromResource(resource, organizationId);
        var result = await personCommandService.Handle(command, cancellationToken);

        return SpaceManagementActionResultAssembler.ToActionResultFromAddPersonResult(
            this, result, problemDetailsFactory,
            person => Created($"/api/v1/organizations/{person.OrganizationId}/people/{person.Id}",
                PersonResourceFromEntityAssembler.ToResourceFromEntity(person)));
    }

    [HttpPut("{personId:long}")]
    [SwaggerOperation(Summary = "Update person", Description = "Update an existing person")]
    [SwaggerResponse(StatusCodes.Status200OK, "Person updated", typeof(PersonResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Person not found")]
    public async Task<IActionResult> UpdatePerson(
        long personId,
        [FromBody] UpdatePersonInformationResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdatePersonInformationCommandFromResourceAssembler.ToCommandFromResource(resource, personId);
        var result = await personCommandService.Handle(command, cancellationToken);

        return SpaceManagementActionResultAssembler.ToActionResultFromAddPersonResult(
            this, result, problemDetailsFactory,
            person => Ok(PersonResourceFromEntityAssembler.ToResourceFromEntity(person)));
    }
}
