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
[Route("api/v1/sites/{siteId:long}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Device endpoints")]
public class DevicesController(
    IDeviceCommandService deviceCommandService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Connect device to site", Description = "Connect a new device to a site")]
    [SwaggerResponse(StatusCodes.Status201Created, "Device connected", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    public async Task<IActionResult> ConnectDeviceToSite(
        long siteId,
        [FromBody] ConnectDeviceToSiteResource resource,
        CancellationToken cancellationToken)
    {
        var command = ConnectDeviceToSiteCommandFromResourceAssembler.ToCommandFromResource(resource, siteId);
        var result = await deviceCommandService.Handle(command, cancellationToken);

        return SpaceManagementActionResultAssembler.ToActionResultFromConnectDeviceResult(
            this, result, problemDetailsFactory,
            device => Created($"/api/v1/sites/{device.SiteId}/devices/{device.Id}",
                DeviceResourceFromEntityAssembler.ToResourceFromEntity(device)));
    }

    [HttpPut("{deviceId:long}")]
    [SwaggerOperation(Summary = "Update device", Description = "Update an existing device")]
    [SwaggerResponse(StatusCodes.Status200OK, "Device updated", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Device not found")]
    public async Task<IActionResult> UpdateDevice(
        long deviceId,
        [FromBody] UpdateDeviceInformationResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateDeviceInformationCommandFromResourceAssembler.ToCommandFromResource(resource, deviceId);
        var result = await deviceCommandService.Handle(command, cancellationToken);

        return SpaceManagementActionResultAssembler.ToActionResultFromConnectDeviceResult(
            this, result, problemDetailsFactory,
            device => Ok(DeviceResourceFromEntityAssembler.ToResourceFromEntity(device)));
    }
}
