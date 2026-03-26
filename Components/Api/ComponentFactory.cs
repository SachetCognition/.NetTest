namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System.Web.Mvc;

    public class ComponentFactory<TComponent>
    {
        public ComponentFactory(HtmlHelper htmlHelper)
        {
            this.HtmlHelper = htmlHelper;
        }

        public ComponentFactory(HtmlHelper htmlHelper, ScriptRenderer.ScriptRendererBuilder scriptRendererBuilder, StyleRender.StyleRendererBuilder styleRendererBuilder)
        {
            this.HtmlHelper = htmlHelper;
        }

        protected HtmlHelper HtmlHelper { get; set; }
    }
}
