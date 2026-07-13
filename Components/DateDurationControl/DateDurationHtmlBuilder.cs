namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateDurationControl
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DateDurationHtmlBuilder : HtmlBuilderBase<DateDurationComponent>
    {
        public DateDurationHtmlBuilder(DateDurationComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
