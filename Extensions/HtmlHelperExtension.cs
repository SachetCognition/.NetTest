using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer;
using Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender;

namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions;

/// <summary>
/// Extension methods for IHtmlHelper to create SAV2000 component factories.
/// </summary>
public static class HtmlHelperExtension
{
    /// <summary>
    /// Creates a SAV2000 component factory for the given HTML helper.
    /// </summary>
    public static ComponentFactory<TModel> Sav2000<TModel>(this IHtmlHelper<TModel> helper)
    {
        return helper.Sav2000(false);
    }

    /// <summary>
    /// Creates a SAV2000 component factory for the given HTML helper.
    /// </summary>
    public static ComponentFactory<TModel> Sav2000<TModel>(this IHtmlHelper<TModel> helper, bool isLightRequirement)
    {
        var clientDependencyScriptRendererComponent = new ClientDependencyScriptRendererComponent(helper);
        var scriptRenderer = helper.ViewContext.HttpContext.Items[ScriptRendererComponent.ContextKey] as ScriptRendererComponent ??
            new ScriptRendererComponent(
                helper,
                new ReadOnlyCollection<IScriptRendererComponent>(new List<IScriptRendererComponent>
                    {
                        clientDependencyScriptRendererComponent,
                        new BlockScriptRendererComponent(helper)
                    }), isLightRequirement);

        var clientDependencyStyleRenderComponent = new ClientDependencyStyleRenderComponent(helper);
        var styleRenderer = helper.ViewContext.HttpContext.Items[StyleRendererComponent.ContextKey] as StyleRendererComponent
                                      ?? new StyleRendererComponent(helper,
                                          new ReadOnlyCollection<IStyleRendererComponent>(
                                          new List<IStyleRendererComponent> { clientDependencyStyleRenderComponent }));

        return new ComponentFactory<TModel>(helper, new ScriptRendererBuilder(scriptRenderer, null), new StyleRendererBuilder(styleRenderer, null));
    }

    /// <summary>
    /// Renders a validation message for the specified expression with accessibility attributes.
    /// </summary>
    public static IHtmlContent Sav2000ValidationMessageFor<TModel, TProperty>(
        this IHtmlHelper<TModel> htmlHelper, 
        Expression<Func<TModel, TProperty>> expression)
    {
        return htmlHelper.ValidationMessageFor(expression, null, new { role = "alert" }, null);
    }
}
