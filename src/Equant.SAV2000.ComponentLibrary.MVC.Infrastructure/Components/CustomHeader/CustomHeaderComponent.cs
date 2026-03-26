namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomHeader
{
    using Microsoft.AspNetCore.Html;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CustomHeaderComponent : ComponentBase
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string CssClass { get; set; }
        public string IconCssClass { get; set; }

        public override IHtmlContent BuildHtml()
        {
            var builder = new CustomHeaderHtmlBuilder(this);
            return builder.Build();
        }

        public override IHtmlContent BuildInitScript()
        {
            return HtmlString.Empty;
        }
    }
}
