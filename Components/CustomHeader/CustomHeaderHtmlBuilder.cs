namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomHeader
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class CustomHeaderHtmlBuilder : HtmlBuilderBase<CustomHeaderComponent>
    {
        public CustomHeaderHtmlBuilder(CustomHeaderComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
