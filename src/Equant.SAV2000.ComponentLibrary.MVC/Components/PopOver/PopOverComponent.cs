namespace Equant.SAV2000.ComponentLibrary.MVC.Components.PopOver
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

    public class PopOverComponent : ComponentBase
    {
        public PopOverComponent() { }
        public PopOverComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Text { get; set; }
        public string Title { get; set; }
        public string CssClass { get; set; }
        public string TriggerElement { get; set; }
        public string Placement { get; set; }

        public override IHtmlContent BuildHtml()
        {
            return new PopOverHtmlBuilder(this).Build();
        }

        public override string BuildInitScript()
        {
            if (string.IsNullOrEmpty(this.Id))
                return string.Empty;
            return string.Format("$('#{0}').popover();", this.Id.JQuerySelectorEscape());
        }
    }
}
