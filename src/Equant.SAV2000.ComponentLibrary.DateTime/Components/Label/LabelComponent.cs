namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Html;

    public class LabelComponent : ComponentBase
    {
        public LabelComponent() { }

        public string Text { get; set; }
        public new string CssClass { get; set; }

        public override IHtmlContent RenderHtml() { return HtmlString.Empty; }
        public override string RenderInitScript() { return string.Empty; }
    }
}
