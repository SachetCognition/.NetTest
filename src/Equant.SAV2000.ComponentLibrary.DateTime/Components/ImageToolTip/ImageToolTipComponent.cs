namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Html;

    public enum PersistanceMode { Click = 0, Hover = 1 }

    public class ImageToolTipComponent : ComponentBase
    {
        public ImageToolTipComponent() { }

        public string Text { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string AlternateText { get; set; }
        public string CssClassImage { get; set; }
        public string CssClassSpan { get; set; }
        public string CssClassInnerSpan { get; set; }
        public string ToolTipId { get; set; }
        public PersistanceMode PersistanceMode { get; set; }

        public override IHtmlContent RenderHtml() { return HtmlString.Empty; }
        public override string RenderInitScript() { return string.Empty; }
    }
}
