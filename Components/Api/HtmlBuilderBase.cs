namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System.Web.UI;

    public abstract class HtmlBuilderBase<TComponent> where TComponent : ComponentBase
    {
        public TComponent Component { get; set; }
        public abstract void Build(HtmlTextWriter writer);
    }
}
