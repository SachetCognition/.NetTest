namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using System.IO;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageHtmlBuilder : HtmlBuilderBase<ImageComponent>
    {
        public ImageHtmlBuilder(ImageComponent component)
        {
            this.Component = component;
        }

        public override void Build(TextWriter writer)
        {
            if (writer != null && this.Component.IsVisible)
            {
                var img = new TagBuilder("img");
                img.Attributes["id"] = this.Component.Id;
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

                foreach (var attr in this.Component.HtmlAttributes)
                {
                    img.MergeAttribute(attr.Key, attr.Value?.ToString());
                }

                img.TagRenderMode = TagRenderMode.SelfClosing;
                img.WriteTo(writer, HtmlEncoder.Default);
            }
        }
    }
}
