namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class LabelComponent : ComponentBase
    {
        public LabelComponent() { }
        public LabelComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Text { get; set; }
        public string CssClass { get; set; }

        public override IHtmlContent BuildHtml()
        {
            return new LabelHtmlBuilder(this).Build();
        }

        public override string BuildInitScript()
        {
            return string.Empty;
        }
    }
}
