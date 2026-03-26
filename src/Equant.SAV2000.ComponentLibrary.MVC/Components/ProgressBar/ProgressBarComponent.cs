namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ProgressBar
{
    using System.IO;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ProgressBarComponent : ComponentBase
    {
        public ProgressBarComponent() : base() { }
        public ProgressBarComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public int Value { get; set; }
        public int Max { get; set; } = 100;
        public string CssClass { get; set; } = "progress-bar";

        public override void WriteHtml(TextWriter writer)
        {
            new ProgressBarHtmlBuilder(this).Build(writer);
        }

        public override void WriteInitScript(TextWriter writer) { }
    }
}
