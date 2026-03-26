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

        public override IHtmlContent WriteHtml()
        {
            return new HtmlString(ToHtmlString());
        }

        public override string WriteInitScript() { return string.Empty; }

        public override string ToHtmlString()
        {
            if (string.IsNullOrEmpty(Text)) return string.Empty;
            var tag = new TagBuilder("span");
            if (!string.IsNullOrEmpty(Id)) tag.MergeAttribute("id", Id);
            if (!string.IsNullOrEmpty(CssClass)) tag.Attributes["class"] = CssClass;
            tag.InnerHtml.Append(Text);
            return tag.ToHtmlString();
        }
    }
}
