namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class CustomLabelHtmlBuilder : HtmlBuilderBase<CustomLabelComponent>
    {
        public CustomLabelHtmlBuilder(CustomLabelComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
