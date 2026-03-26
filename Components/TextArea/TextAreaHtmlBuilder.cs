namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextArea
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class TextAreaHtmlBuilder : HtmlBuilderBase<TextAreaComponent>
    {
        public TextAreaHtmlBuilder(TextAreaComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
