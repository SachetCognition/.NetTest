namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Html;

    public class HyperLinkComponent : ComponentBase
    {
        public HyperLinkComponent() { }
        public string ActionUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public override IHtmlContent RenderHtml() { return HtmlString.Empty; }
        public override string RenderInitScript() { return string.Empty; }
    }
}
