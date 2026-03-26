namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DialogBox
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DialogBoxHtmlBuilder : HtmlBuilderBase<DialogBoxComponent>
    {
        public DialogBoxHtmlBuilder(DialogBoxComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
