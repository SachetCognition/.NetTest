namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ListBox
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ListBoxHtmlBuilder : HtmlBuilderBase<ListBoxComponent>
    {
        public ListBoxHtmlBuilder(ListBoxComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
