namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageToolTipComponent : ComponentBase
    {
        public ImageToolTipComponent() { }
        public ImageToolTipComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string ToolTipId { get; set; }
        public string ImageAlt { get; set; }
        public string CssClass { get; set; }
        public string CssClassSpan { get; set; }
        public string CssClassInnerSpan { get; set; }
        public string Title { get; set; }
        public PersistanceMode PersistanceMode { get; set; }

        public override IHtmlContent BuildHtml()
        {
            return new ImageToolTipHtmlBuilder(this).Build();
        }

        public override string BuildInitScript()
        {
            return string.Empty;
        }
    }
}
