namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DialogBox;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomHeader;

    public class ComponentFactory<TModel>
    {
        public IHtmlHelper<TModel> HtmlHelper { get; set; }
        public ScriptRendererBuilder ScriptRendererBuilder { get; set; }
        public StyleRendererBuilder StyleRendererBuilder { get; set; }
        public bool IsLightRequirement { get; set; }

        public ComponentFactory(IHtmlHelper<TModel> htmlHelper)
        {
            this.HtmlHelper = htmlHelper;
        }

        public ComponentFactory(IHtmlHelper<TModel> htmlHelper, bool isLightRequirement)
        {
            this.HtmlHelper = htmlHelper;
            this.IsLightRequirement = isLightRequirement;
        }

        public ComponentFactory(IHtmlHelper<TModel> htmlHelper, ScriptRendererBuilder scriptRendererBuilder, StyleRendererBuilder styleRendererBuilder)
        {
            this.HtmlHelper = htmlHelper;
            this.ScriptRendererBuilder = scriptRendererBuilder;
            this.StyleRendererBuilder = styleRendererBuilder;
        }

        public DialogBoxBuilder DialogBox()
        {
            return new DialogBoxBuilder(new DialogBoxComponent());
        }

        public CustomHeaderBuilder CustomHeader()
        {
            return new CustomHeaderBuilder(new CustomHeaderComponent());
        }

        public ScriptRendererBuilder ScriptRenderer()
        {
            return this.ScriptRendererBuilder ?? new ScriptRendererBuilder(new ScriptRendererComponent());
        }

        public StyleRendererBuilder StyleRenderer()
        {
            return this.StyleRendererBuilder ?? new StyleRendererBuilder(new StyleRendererComponent());
        }
    }
}
