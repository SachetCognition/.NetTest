namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Duration
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class DurationHtmlBuilder : HtmlBuilderBase<DurationComponent>
    {
        public DurationHtmlBuilder(DurationComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
