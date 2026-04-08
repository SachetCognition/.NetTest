namespace Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class SpanLabelComponent : ComponentBase
    {
        public SpanLabelComponent() { }
        public SpanLabelComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Text { get; set; }
        public string CssClass { get; set; }

        public override IHtmlContent BuildHtml()
        {
            return new SpanLabelHtmlBuilder(this).Build();
        }

        public override string BuildInitScript()
        {
            return string.Empty;
        }
    }
}
