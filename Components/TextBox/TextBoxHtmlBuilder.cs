namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextBox
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class TextBoxHtmlBuilder : HtmlBuilderBase<TextBoxComponent>
    {
        public TextBoxHtmlBuilder(TextBoxComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
