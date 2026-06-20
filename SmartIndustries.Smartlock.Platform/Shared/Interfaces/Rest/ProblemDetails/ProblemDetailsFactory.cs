using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SmartIndustries.Smartlock.Platform.Shared.Resources;
using SmartIndustries.Smartlock.Platform.Shared.Resources.Errors;

namespace SmartIndustries.Smartlock.Platform.Shared.Interfaces.Rest.ProblemDetails;

public class ProblemDetailsFactory
{
    private readonly Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory _aspNetCoreFactory;
    private readonly IStringLocalizer<CommonMessages> _commonLocalizer;
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer;

    public ProblemDetailsFactory(
        IStringLocalizer<ErrorMessages> errorLocalizer,
        IStringLocalizer<CommonMessages> commonLocalizer,
        Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory aspNetCoreFactory)
    {
        _errorLocalizer = errorLocalizer;
        _commonLocalizer = commonLocalizer;
        _aspNetCoreFactory = aspNetCoreFactory;
    }

    public IActionResult CreateProblemDetails(ControllerBase controller, int statusCode, Enum? errorEnum, string detailMessage)
    {
        var pd = _aspNetCoreFactory.CreateProblemDetails(controller.HttpContext, statusCode,
            errorEnum != null ? _errorLocalizer[$"{errorEnum.GetType().Name}.{errorEnum}"] : _commonLocalizer["GenericError"],
            detail: detailMessage);

        if (pd == null)
        {
            pd = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = statusCode,
                Title = errorEnum != null ? _errorLocalizer[$"{errorEnum.GetType().Name}.{errorEnum}"] : _commonLocalizer["GenericError"],
                Detail = detailMessage,
                Instance = controller.HttpContext.Request.Path
            };
        }
        else
        {
            pd.Title = errorEnum != null ? _errorLocalizer[$"{errorEnum.GetType().Name}.{errorEnum}"] : _commonLocalizer["GenericError"];
            pd.Detail = detailMessage;
            pd.Instance = controller.HttpContext.Request.Path;
        }
        return controller.StatusCode(statusCode, pd);
    }

    public IActionResult CreateProblemDetails(ControllerBase controller, int statusCode, string titleKey, string detailKey, params object[] detailArgs)
    {
        var pd = _aspNetCoreFactory.CreateProblemDetails(controller.HttpContext, statusCode,
            _commonLocalizer[titleKey],
            detail: _errorLocalizer[detailKey, detailArgs],
            instance: controller.HttpContext.Request.Path);
        return controller.StatusCode(statusCode, pd);
    }
}
