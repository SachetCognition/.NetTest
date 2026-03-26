namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextBox
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class TextBoxHtmlBuilder : HtmlBuilderBase<TextBoxComponent>
    {
        public TextBoxHtmlBuilder(TextBoxComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
