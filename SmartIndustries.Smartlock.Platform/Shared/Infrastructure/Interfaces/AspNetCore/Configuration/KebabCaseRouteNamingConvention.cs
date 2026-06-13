using Microsoft.AspNetCore.Mvc.ApplicationModels;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration.Extensions;

namespace SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;

public class KebabCaseRouteNamingConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
            selector.AttributeRouteModel = ReplaceTemplate(selector, controller.ControllerName);
        foreach (var selector in controller.Actions.SelectMany(a => a.Selectors))
            selector.AttributeRouteModel = ReplaceTemplate(selector, controller.ControllerName);
    }

    private static AttributeRouteModel? ReplaceTemplate(SelectorModel selector, string name)
        => selector.AttributeRouteModel is not null
            ? new AttributeRouteModel { Template = selector.AttributeRouteModel.Template?.Replace("[controller]", name.ToKebabCase()) }
            : null;
}
