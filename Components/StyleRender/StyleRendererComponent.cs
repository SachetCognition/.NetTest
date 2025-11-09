namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender
{
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class StyleRendererComponent : ComponentBase
    {
        public const string ContextKey = "StyleRendererComponent";

        public StyleRendererComponent(IHtmlHelper htmlHelper, ReadOnlyCollection<object> renderers)
            : base(htmlHelper)
        {
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new System.Collections.Generic.List<JsResource>()); }
        }

        public override void WriteHtml(TextWriter writer)
        {
        }

        public override void WriteInitScript(TextWriter writer)
        {
        }
    }

    public class StyleRendererBuilder : ComponentBuilderBase<StyleRendererComponent, StyleRendererBuilder>
    {
        public StyleRendererBuilder(StyleRendererComponent component, Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }

    public class ClientDependencyStyleRenderComponent
    {
        public ClientDependencyStyleRenderComponent(IHtmlHelper htmlHelper)
        {
        }
    }
}
