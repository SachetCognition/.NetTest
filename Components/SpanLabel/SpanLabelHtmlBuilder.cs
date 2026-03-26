namespace Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class SpanLabelHtmlBuilder : HtmlBuilderBase<SpanLabelComponent>
    {
        public SpanLabelHtmlBuilder(SpanLabelComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
