namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HyperLinkComponent : ComponentBase
    {
        public HyperLinkComponent() { }
        public HyperLinkComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Text { get; set; }
        public string Href { get; set; }
        public string CssClass { get; set; }
        public string Target { get; set; }
        public string Title { get; set; }
        public string ActionUrl { get; set; }
        public string ImageUrl { get; set; }

        public override IHtmlContent BuildHtml()
        {
            return new HyperLinkHtmlBuilder(this).Build();
        }

        public override string BuildInitScript()
        {
            return string.Empty;
        }
    }
}
