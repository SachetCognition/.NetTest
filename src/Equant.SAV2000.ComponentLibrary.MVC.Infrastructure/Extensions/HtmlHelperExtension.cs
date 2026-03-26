namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender;

    /// <summary>
    /// Extension methods for IHtmlHelper providing the Sav2000 component factory entry point.
    /// </summary>
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// SAV2000 component factory entry point.
        /// Usage: Html.Sav2000().ComponentName().Property(value)
        /// </summary>
        public static ComponentFactory<TModel> Sav2000<TModel>(this IHtmlHelper<TModel> helper)
        {
            return helper.Sav2000(false);
        }

        /// <summary>
        /// SAV2000 component factory with light requirement mode.
        /// Usage: Html.Sav2000(true).ComponentName().Property(value)
        /// </summary>
        public static ComponentFactory<TModel> Sav2000<TModel>(this IHtmlHelper<TModel> helper, bool isLightRequirement)
        {
            var scriptRenderer = new ScriptRendererComponent(helper,
                new ReadOnlyCollection<IScriptRendererComponent>(new List<IScriptRendererComponent>
                {
                    new ClientDependencyScriptRendererComponent(helper),
                    new BlockScriptRendererComponent(helper)
                }), isLightRequirement);

            var styleRenderer = new StyleRendererComponent(helper,
                new ReadOnlyCollection<IStyleRendererComponent>(
                    new List<IStyleRendererComponent> { new ClientDependencyStyleRenderComponent(helper) }));

            var factory = new ComponentFactory<TModel>(
                helper,
                new ScriptRendererBuilder(scriptRenderer, null),
                new StyleRendererBuilder(styleRenderer, null));
            factory.IsLightRequirement = isLightRequirement;

            return factory;
        }
    }
}
