namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CustomLabelHtmlBuilder : HtmlBuilderBase<CustomLabelComponent>
    {
        public CustomLabelHtmlBuilder(CustomLabelComponent component) : base(component) { }
        public override void Build(HtmlTextWriter writer) { }
    }
}
