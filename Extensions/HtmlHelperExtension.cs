// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHelperExtension.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the HtmlHelperExtension type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.SessionState;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender;

    /// <summary>
    /// The html helper extension.
    /// </summary>
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// SAV2000 component factory
        /// </summary>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>ComponentFactory</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static ComponentFactory<TModel> Sav2000<TModel>(this HtmlHelper<TModel> helper)
        {
            return helper.Sav2000(false);
        }

        /// <summary>
        /// The SAV 2000.
        /// </summary>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <param name="isLightRequirement">
        /// if view required only lightened scripts.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <returns>
        /// The <see>
        ///         <cref>ComponentFactory</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static ComponentFactory<TModel> Sav2000<TModel>(this HtmlHelper<TModel> helper, bool isLightRequirement)
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
        /// The validation message for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString  Sav2000ValidationMessageFor<TModel,TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
          return   htmlHelper.ValidationMessageFor(expression,null,new {role="alert"}  );
        }
    }
}
