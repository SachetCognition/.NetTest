namespace Equant.SAV2000.ComponentLibrary.MVC.Components.PopupImage
{
    using System.IO;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class PopupImageHtmlBuilder : HtmlBuilderBase<PopupImageComponent>
    {
        public PopupImageHtmlBuilder(PopupImageComponent component)
        {
            this.Component = component;
        }

        public override void Build(TextWriter writer)
        {
            if (writer != null && this.Component.IsVisible)
            {
                var anchor = new TagBuilder("a");
                anchor.Attributes["id"] = this.Component.Id;
                anchor.Attributes["href"] = this.Component.PopupUrl ?? this.Component.Src ?? "#";
                anchor.Attributes["title"] = this.Component.PopupTitle ?? this.Component.Title ?? string.Empty;
                anchor.AddCssClass("popup-image-trigger");

                var img = new TagBuilder("img");
                img.Attributes["src"] = this.Component.Src ?? string.Empty;
                img.Attributes["alt"] = this.Component.Alt ?? string.Empty;

                if (!string.IsNullOrEmpty(this.Component.Title))
                {
                    img.Attributes["title"] = this.Component.Title;
                }

                if (this.Component.Width.HasValue)
                {
                    img.Attributes["width"] = this.Component.Width.Value.ToString();
                }

                if (this.Component.Height.HasValue)
                {
                    img.Attributes["height"] = this.Component.Height.Value.ToString();
                }

                img.TagRenderMode = TagRenderMode.SelfClosing;

                using (var sw = new StringWriter())
                {
                    img.WriteTo(sw, HtmlEncoder.Default);
                    anchor.InnerHtml.AppendHtml(sw.ToString());
                }

                foreach (var attr in this.Component.HtmlAttributes)
                {
                    anchor.MergeAttribute(attr.Key, attr.Value?.ToString());
                }

                anchor.WriteTo(writer, HtmlEncoder.Default);
            }
        }
    }
}
