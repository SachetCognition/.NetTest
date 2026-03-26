namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class LabelComponent : ComponentBase
    {

        public LabelComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }
        public string Text { get; set; }
        public string CssClass { get; set; }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }
}
