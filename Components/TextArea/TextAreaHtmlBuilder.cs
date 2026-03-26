namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextArea
{
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class TextAreaHtmlBuilder : HtmlBuilderBase<TextAreaComponent>
    {
        public TextAreaHtmlBuilder(TextAreaComponent component) : base(component) { }
        public override void Build(HtmlTextWriter writer) { }
    }
}
