namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ComponentFactory<TModel>
    {
        public IHtmlHelper<TModel> HtmlHelper { get; set; }

        public ComponentFactory(IHtmlHelper<TModel> htmlHelper) { this.HtmlHelper = htmlHelper; }
    }
}
