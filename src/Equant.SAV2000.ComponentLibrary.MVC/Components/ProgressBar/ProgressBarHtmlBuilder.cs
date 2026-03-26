namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ProgressBar
{
    using System.IO;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ProgressBarHtmlBuilder : HtmlBuilderBase<ProgressBarComponent>
    {
        public ProgressBarHtmlBuilder(ProgressBarComponent component)
        {
            this.Component = component;
        }

        public override void Build(TextWriter writer)
        {
            if (writer != null && this.Component.IsVisible)
            {
                var progress = new TagBuilder("progress");
                progress.Attributes["id"] = this.Component.Id;
                progress.Attributes["value"] = this.Component.Value.ToString();
                progress.Attributes["max"] = this.Component.Max.ToString();

                if (!string.IsNullOrEmpty(this.Component.CssClass))
                {
                    progress.AddCssClass(this.Component.CssClass);
                }

                foreach (var attr in this.Component.HtmlAttributes)
                {
                    progress.MergeAttribute(attr.Key, attr.Value?.ToString());
                }

                progress.InnerHtml.Append(this.Component.Value + "%");
                progress.WriteTo(writer, HtmlEncoder.Default);
            }
        }
    }
}
