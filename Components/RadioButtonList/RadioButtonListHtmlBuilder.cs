namespace Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButtonList
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class RadioButtonListHtmlBuilder : HtmlBuilderBase<RadioButtonListComponent>
    {
        public RadioButtonListHtmlBuilder(RadioButtonListComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
