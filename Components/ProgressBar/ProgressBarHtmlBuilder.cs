namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ProgressBar
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class ProgressBarHtmlBuilder : HtmlBuilderBase<ProgressBarComponent>
    {
        public ProgressBarHtmlBuilder(ProgressBarComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
