namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextArea
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class TextAreaComponent : ComponentBase
    {
        public string Value { get; set; }
        public string CssClass { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }
}
