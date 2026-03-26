namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System.Web.UI;

    public abstract class HtmlBuilderBase<TComponent> : IHtmlBuilder
        where TComponent : ComponentBase
    {
        protected TComponent Component { get; set; }

        public abstract void Build(HtmlTextWriter writer);
    }
}
