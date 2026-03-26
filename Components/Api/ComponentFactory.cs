namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender;

    public class ComponentFactory<TModel>
    {
        public IHtmlHelper<TModel> HtmlHelper { get; set; }
        public ScriptRendererBuilder ScriptRendererBuilder { get; set; }
        public StyleRendererBuilder StyleRendererBuilder { get; set; }

        public ComponentFactory(IHtmlHelper<TModel> htmlHelper) { this.HtmlHelper = htmlHelper; }
        public ComponentFactory(IHtmlHelper<TModel> htmlHelper, ScriptRendererBuilder scriptRendererBuilder, StyleRendererBuilder styleRendererBuilder)
        {
            this.HtmlHelper = htmlHelper;
            this.ScriptRendererBuilder = scriptRendererBuilder;
            this.StyleRendererBuilder = styleRendererBuilder;
        }
    }
}
