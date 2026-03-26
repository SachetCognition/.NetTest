namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class LabelHtmlBuilder : HtmlBuilderBase<LabelComponent>
    {
        public LabelHtmlBuilder(LabelComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
