namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextBox
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class TextBoxComponent : ComponentBase
    {

        public TextBoxComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }
        public string Value { get; set; }
        public string CssClass { get; set; }
        public string Placeholder { get; set; }
        public int MaxLength { get; set; }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }
}
